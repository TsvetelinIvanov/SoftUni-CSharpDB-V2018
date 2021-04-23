using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace MiniORM
{
	/// <summary>
	/// Used for accessing a database, inserting/updating/deleting entities
	/// and mapping database columns to entity classes.
	/// </summary>
	internal class DatabaseConnection
	{
		private readonly SqlConnection connection;
		private SqlTransaction transaction;

		public DatabaseConnection(string connectionString)
		{
			this.connection = new SqlConnection(connectionString);
		}

        public void Open() => this.connection.Open();

        public void Close() => this.connection.Close();

        public SqlTransaction StartTransaction()
        {
            this.transaction = this.connection.BeginTransaction();

            return this.transaction;
        }        

        public int ExecuteNonQuery(string queryText, params SqlParameter[] parameters)
		{
			using (SqlCommand query = this.CreateCommand(queryText, parameters))
			{
				int result = query.ExecuteNonQuery();

				return result;
			}
		}

		public IEnumerable<string> FetchColumnNames(string tableName)
		{
			List<string> rows = new List<string>();
			string queryText = $@"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'";
			using (SqlCommand query = this.CreateCommand(queryText))
			{
				using (SqlDataReader reader = query.ExecuteReader())
				{
					while (reader.Read())
					{
						string column = reader.GetString(0);
						rows.Add(column);
					}
				}
			}

			return rows;
		}

		public IEnumerable<T> ExecuteQuery<T>(string queryText)
		{
			List<T> rows = new List<T>();
			using (SqlCommand query = this.CreateCommand(queryText))
			{
				using (SqlDataReader reader = query.ExecuteReader())
				{
					while (reader.Read())
					{
						object[] columnValues = new object[reader.FieldCount];
						reader.GetValues(columnValues);

						T objectT = reader.GetFieldValue<T>(0);
						rows.Add(objectT);
					}
				}
			}

			return rows;
		}

		public IEnumerable<T> FetchResultSet<T>(string tableName, params string[] columnNames)
		{
			List<T> rows = new List<T>();
			string escapedColumnsString = string.Join(", ", columnNames.Select(EscapeColumn));
			string queryText = $@"SELECT {escapedColumnsString} FROM {tableName}";
			using (SqlCommand query = this.CreateCommand(queryText))
			{
				using (SqlDataReader reader = query.ExecuteReader())
				{
					while (reader.Read())
					{
						object[] columnValues = new object[reader.FieldCount];
						reader.GetValues(columnValues);

						T objectT = MapColumnsToObject<T>(columnNames, columnValues);
						rows.Add(objectT);
					}
				}
			}

			return rows;
		}

		public void InsertEntities<T>(IEnumerable<T> entities, string tableName, string[] columns) where T : class
		{
			IEnumerable<string> identityColumns = this.GetIdentityColumns(tableName);
			string[] columnsToInsert = columns.Except(identityColumns).ToArray();
			string[] escapedColumns = columnsToInsert.Select(EscapeColumn).ToArray();

			object[][] rowValues = entities
				.Select(entity => columnsToInsert
					.Select(c => entity.GetType().GetProperty(c).GetValue(entity))
					.ToArray())
				.ToArray();

			string[][] rowParameterNames = Enumerable.Range(1, rowValues.Length)
				.Select(i => columnsToInsert.Select(c => c + i).ToArray())
				.ToArray();

			string sqlColumnsString = string.Join(", ", escapedColumns);

			string sqlRowsString = string.Join(", ", rowParameterNames.Select(p => string.Format("({0})", string.Join(", ", p.Select(a => $"@{a}")))));

			string queryString = string.Format("INSERT INTO {0} ({1}) VALUES {2}", tableName, sqlColumnsString, sqlRowsString);

			SqlParameter[] parameters = rowParameterNames
				.Zip(rowValues, (@params, values) => @params.Zip(values, (param, value) => new SqlParameter(param, value ?? DBNull.Value)))
				.SelectMany(p => p)
				.ToArray();

			int insertedRows = this.ExecuteNonQuery(queryString, parameters);
			if (insertedRows != entities.Count())
			{
				throw new InvalidOperationException($"Could not insert {entities.Count() - insertedRows} rows.");
			}
		}

		public void UpdateEntities<T>(IEnumerable<T> modifiedEntities, string tableName, string[] columns) where T : class
		{
			IEnumerable<string> identityColumns = this.GetIdentityColumns(tableName);
			string[] columnsToUpdate = columns.Except(identityColumns).ToArray();

			PropertyInfo[] primaryKeyProperties = typeof(T).GetProperties()
				.Where(pi => pi.HasAttribute<KeyAttribute>())
				.ToArray();

			foreach (T entity in modifiedEntities)
			{
				object[] primaryKeyValues = primaryKeyProperties.Select(c => c.GetValue(entity)).ToArray();
				SqlParameter[] primaryKeyParameters = primaryKeyProperties.Zip(primaryKeyValues, (param, value) => new SqlParameter(param.Name, value)).ToArray();

				object[] rowValues = columnsToUpdate.Select(c => entity.GetType().GetProperty(c).GetValue(entity) ?? DBNull.Value).ToArray();
				SqlParameter[] columnsParameters = columnsToUpdate.Zip(rowValues, (param, value) => new SqlParameter(param, value)).ToArray();

				string columnsSqlString = string.Join(", ", columnsToUpdate.Select(c => $"{c} = @{c}"));
				string primaryKeysSqlString = string.Join(" AND ",
					primaryKeyProperties.Select(pk => $"{pk.Name} = @{pk.Name}"));
				string queryString = string.Format("UPDATE {0} SET {1} WHERE {2}", tableName, columnsSqlString, primaryKeysSqlString);

				int updatedRows = this.ExecuteNonQuery(queryString, columnsParameters.Concat(primaryKeyParameters).ToArray());
				if (updatedRows != 1)
				{
					throw new InvalidOperationException($"Update for table {tableName} failed.");
				}
			}
		}

		public void DeleteEntities<T>(IEnumerable<T> entitiesToDelete, string tableName, string[] columns) where T : class
		{
			PropertyInfo[] primaryKeyProperties = typeof(T).GetProperties().Where(pi => pi.HasAttribute<KeyAttribute>()).ToArray();

			foreach (T entity in entitiesToDelete)
			{
				object[] primaryKeyValues = primaryKeyProperties.Select(c => c.GetValue(entity)).ToArray();
				SqlParameter[] primaryKeyParameters = primaryKeyProperties.Zip(primaryKeyValues, (param, value) => new SqlParameter(param.Name, value)).ToArray();

				string primaryKeysSqlString = string.Join(" AND ", primaryKeyProperties.Select(pk => $"{pk.Name} = @{pk.Name}"));
				string queryString = string.Format("DELETE FROM {0} WHERE {1}", tableName, primaryKeysSqlString);

				int updatedRows = this.ExecuteNonQuery(queryString, primaryKeyParameters);
				if (updatedRows != 1)
				{
					throw new InvalidOperationException($"Delete for table {tableName} failed.");
				}
			}
		}				

        private SqlCommand CreateCommand(string queryText, params SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(queryText, this.connection, this.transaction);

            foreach (SqlParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }

            return command;
        }

        private IEnumerable<string> GetIdentityColumns(string tableName)
        {
            const string identityColumnsSqlString = "SELECT COLUMN_NAME FROM (SELECT COLUMN_NAME, COLUMNPROPERTY(OBJECT_ID(TABLE_NAME), COLUMN_NAME, 'IsIdentity') AS IsIdentity FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{0}') AS IdentitySpecs WHERE IsIdentity = 1";

            string parametrizedSqlString = string.Format(identityColumnsSqlString, tableName);

            IEnumerable<string> identityColumns = ExecuteQuery<string>(parametrizedSqlString);

            return identityColumns;
        }

        private static string EscapeColumn(string column)
		{
			string escapedColumn = $"[{column}]";

			return escapedColumn;
		}

		private static T MapColumnsToObject<T>(string[] columnNames, object[] columns)
		{
			T objectT = Activator.CreateInstance<T>();

			for (int i = 0; i < columns.Length; i++)
			{
				string columnName = columnNames[i];
				object columnValue = columns[i];
				if (columnValue is DBNull)
				{
					columnValue = null;
				}

				PropertyInfo property = typeof(T).GetProperty(columnName);
				property.SetValue(objectT, columnValue);
			}

			return objectT;
		}
	}
}