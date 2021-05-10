-- Create the database [Geography] if it does not exist
IF NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = 'Geography')
  CREATE DATABASE Geography
GO

USE Geography
GO


-- Drop all existing Geography tables, so that we can create them
IF OBJECT_ID('Monasteries') IS NOT NULL
  DROP TABLE Monasteries
IF OBJECT_ID('CountriesRivers') IS NOT NULL
  DROP TABLE CountriesRivers
IF OBJECT_ID('MountainsCountries') IS NOT NULL
  DROP TABLE MountainsCountries
IF OBJECT_ID('Peaks') IS NOT NULL
  DROP TABLE Peaks
IF OBJECT_ID('Mountains') IS NOT NULL
  DROP TABLE Mountains
IF OBJECT_ID('Rivers') IS NOT NULL
  DROP TABLE Rivers
IF OBJECT_ID('Countries') IS NOT NULL
  DROP TABLE Countries
IF OBJECT_ID('Continents') IS NOT NULL
  DROP TABLE Continents
IF OBJECT_ID('Currencies') IS NOT NULL
  DROP TABLE Currencies


-- Create tables
CREATE TABLE Continents(
	ContinentCode char(2) NOT NULL,
	ContinentName nvarchar(50) NOT NULL,
  CONSTRAINT PK_Continents PRIMARY KEY CLUSTERED (ContinentCode)
)
GO

CREATE TABLE Countries(
	CountryCode char(2) NOT NULL,
	IsoCode char(3) NOT NULL,
	CountryName varchar(45) NOT NULL,
	CurrencyCode char(3),
	ContinentCode char(2) NOT NULL,
	Population int NOT NULL,
	AreaInSqKm int NOT NULL,
	Capital varchar(30) NOT NULL,
  CONSTRAINT PK_Countries PRIMARY KEY CLUSTERED (CountryCode)
 )
GO

CREATE TABLE Currencies(
	CurrencyCode char(3) NOT NULL,
	Description nvarchar(200) NOT NULL,
  CONSTRAINT PK_Currencies PRIMARY KEY CLUSTERED (CurrencyCode)
)
GO

CREATE TABLE Mountains(
	Id int IDENTITY NOT NULL,
	MountainRange nvarchar(50) NOT NULL,
  CONSTRAINT PK_Mountains PRIMARY KEY CLUSTERED (Id)
)
GO

CREATE TABLE MountainsCountries(
	MountainId int NOT NULL,
	CountryCode char(2) NOT NULL,
  CONSTRAINT PK_MountainsContinents PRIMARY KEY CLUSTERED (MountainId, CountryCode)
)
GO

CREATE TABLE Peaks(
	Id int IDENTITY NOT NULL,
	PeakName nvarchar(50) NOT NULL,
	Elevation int NOT NULL,
	MountainId int NOT NULL,
  CONSTRAINT PK_Peaks PRIMARY KEY CLUSTERED (Id)
)
GO

CREATE TABLE Rivers(
	Id int IDENTITY NOT NULL,
	RiverName nvarchar(50) NOT NULL,
	Length int NOT NULL,
	DrainageArea int NOT NULL,
	AverageDischarge int NOT NULL,
	Outflow nvarchar(50) NOT NULL,
  CONSTRAINT PK_Rivers PRIMARY KEY CLUSTERED (Id)
)
GO

CREATE TABLE CountriesRivers(
	RiverId int NOT NULL,
	CountryCode char(2) NOT NULL,
  CONSTRAINT PK_CountriesRivers PRIMARY KEY CLUSTERED (CountryCode, RiverId)
)
GO


-- Insert table data
INSERT Continents (ContinentCode, ContinentName) VALUES
(N'AF', N'Africa'),
(N'AN', N'Antarctica'),
(N'AS', N'Asia'),
(N'EU', N'Europe'),
(N'NA', N'North America'),
(N'OC', N'Oceania'),
(N'SA', N'South America')

INSERT Countries (CountryCode, IsoCode, CountryName, CurrencyCode, ContinentCode, Population, AreaInSqKm, Capital) VALUES
(N'AD', N'AND', N'Andorra', N'EUR', N'EU', 84000, 468, N'Andorra la Vella'),
(N'AE', N'ARE', N'United Arab Emirates', N'AED', N'AS', 4975593, 82880, N'Abu Dhabi'),
(N'AF', N'AFG', N'Afghanistan', N'AFN', N'AS', 29121286, 647500, N'Kabul'),
(N'AG', N'ATG', N'Antigua and Barbuda', N'XCD', N'NA', 86754, 443, N'St. John''s'),
(N'AI', N'AIA', N'Anguilla', N'XCD', N'NA', 13254, 102, N'The Valley'),
(N'AL', N'ALB', N'Albania', N'ALL', N'EU', 2986952, 28748, N'Tirana'),
(N'AM', N'ARM', N'Armenia', N'AMD', N'AS', 2968000, 29800, N'Yerevan'),
(N'AO', N'AGO', N'Angola', N'AOA', N'AF', 13068161, 1246700, N'Luanda'),
(N'AQ', N'ATA', N'Antarctica', NULL, N'AN', 0, 14000000, N''),
(N'AR', N'ARG', N'Argentina', N'ARS', N'SA', 41343201, 2766890, N'Buenos Aires'),
(N'AS', N'ASM', N'American Samoa', N'USD', N'OC', 57881, 199, N'Pago Pago'),
(N'AT', N'AUT', N'Austria', N'EUR', N'EU', 8205000, 83858, N'Vienna'),
(N'AU', N'AUS', N'Australia', N'AUD', N'OC', 21515754, 7686850, N'Canberra'),
(N'AW', N'ABW', N'Aruba', N'AWG', N'NA', 71566, 193, N'Oranjestad'),
(N'AX', N'ALA', N'?land', N'EUR', N'EU', 26711, 1580, N'Mariehamn'),
(N'AZ', N'AZE', N'Azerbaijan', N'AZN', N'AS', 8303512, 86600, N'Baku'),
(N'BA', N'BIH', N'Bosnia and Herzegovina', N'BAM', N'EU', 4590000, 51129, N'Sarajevo'),
(N'BB', N'BRB', N'Barbados', N'BBD', N'NA', 285653, 431, N'Bridgetown'),
(N'BD', N'BGD', N'Bangladesh', N'BDT', N'AS', 156118464, 144000, N'Dhaka'),
(N'BE', N'BEL', N'Belgium', N'EUR', N'EU', 10403000, 30510, N'Brussels'),
(N'BF', N'BFA', N'Burkina Faso', N'XOF', N'AF', 16241811, 274200, N'Ouagadougou'),
(N'BG', N'BGR', N'Bulgaria', N'BGN', N'EU', 7148785, 110910, N'Sofia'),
(N'BH', N'BHR', N'Bahrain', N'BHD', N'AS', 738004, 665, N'Manama'),
(N'BI', N'BDI', N'Burundi', N'BIF', N'AF', 9863117, 27830, N'Bujumbura'),
(N'BJ', N'BEN', N'Benin', N'XOF', N'AF', 9056010, 112620, N'Porto-Novo'),
(N'BL', N'BLM', N'Saint Barth?lemy', N'EUR', N'NA', 8450, 21, N'Gustavia'),
(N'BM', N'BMU', N'Bermuda', N'BMD', N'NA', 65365, 53, N'Hamilton'),
(N'BN', N'BRN', N'Brunei', N'BND', N'AS', 395027, 5770, N'Bandar Seri Begawan'),
(N'BO', N'BOL', N'Bolivia', N'BOB', N'SA', 9947418, 1098580, N'Sucre'),
(N'BQ', N'BES', N'Bonaire', N'USD', N'NA', 18012, 328, N''),
(N'BR', N'BRA', N'Brazil', N'BRL', N'SA', 201103330, 8511965, N'Bras?lia'),
(N'BS', N'BHS', N'Bahamas', N'BSD', N'NA', 301790, 13940, N'Nassau'),
(N'BT', N'BTN', N'Bhutan', N'BTN', N'AS', 699847, 47000, N'Thimphu'),
(N'BV', N'BVT', N'Bouvet Island', N'NOK', N'AN', 0, 49, N''),
(N'BW', N'BWA', N'Botswana', N'BWP', N'AF', 2029307, 600370, N'Gaborone'),
(N'BY', N'BLR', N'Belarus', N'BYR', N'EU', 9685000, 207600, N'Minsk'),
(N'BZ', N'BLZ', N'Belize', N'BZD', N'NA', 314522, 22966, N'Belmopan'),
(N'CA', N'CAN', N'Canada', N'CAD', N'NA', 33679000, 9984670, N'Ottawa'),
(N'CC', N'CCK', N'Cocos Islands', N'AUD', N'AS', 628, 14, N'West Island'),
(N'CD', N'COD', N'Democratic Republic of the Congo', N'CDF', N'AF', 70916439, 2345410, N'Kinshasa'),
(N'CF', N'CAF', N'Central African Republic', N'XAF', N'AF', 4844927, 622984, N'Bangui'),
(N'CG', N'COG', N'Republic of the Congo', N'XAF', N'AF', 3039126, 342000, N'Brazzaville'),
(N'CH', N'CHE', N'Switzerland', N'CHF', N'EU', 7581000, 41290, N'Berne'),
(N'CI', N'CIV', N'Ivory Coast', N'XOF', N'AF', 21058798, 322460, N'Yamoussoukro'),
(N'CK', N'COK', N'Cook Islands', N'NZD', N'OC', 21388, 240, N'Avarua'),
(N'CL', N'CHL', N'Chile', N'CLP', N'SA', 16746491, 756950, N'Santiago'),
(N'CM', N'CMR', N'Cameroon', N'XAF', N'AF', 19294149, 475440, N'Yaound?'),
(N'CN', N'CHN', N'China', N'CNY', N'AS', 1330044000, 9596960, N'Beijing'),
(N'CO', N'COL', N'Colombia', N'COP', N'SA', 47790000, 1138910, N'Bogot?'),
(N'CR', N'CRI', N'Costa Rica', N'CRC', N'NA', 4516220, 51100, N'San Jos?'),
(N'CU', N'CUB', N'Cuba', N'CUP', N'NA', 11423000, 110860, N'Havana'),
(N'CV', N'CPV', N'Cape Verde', N'CVE', N'AF', 508659, 4033, N'Praia'),
(N'CW', N'CUW', N'Curacao', N'ANG', N'NA', 141766, 444, N'Willemstad'),
(N'CX', N'CXR', N'Christmas Island', N'AUD', N'AS', 1500, 135, N'The Settlement'),
(N'CY', N'CYP', N'Cyprus', N'EUR', N'EU', 1102677, 9250, N'Nicosia'),
(N'CZ', N'CZE', N'Czech Republic', N'CZK', N'EU', 10476000, 78866, N'Prague'),
(N'DE', N'DEU', N'Germany', N'EUR', N'EU', 81802257, 357021, N'Berlin'),
(N'DJ', N'DJI', N'Djibouti', N'DJF', N'AF', 740528, 23000, N'Djibouti'),
(N'DK', N'DNK', N'Denmark', N'DKK', N'EU', 5484000, 43094, N'Copenhagen'),
(N'DM', N'DMA', N'Dominica', N'XCD', N'NA', 72813, 754, N'Roseau'),
(N'DO', N'DOM', N'Dominican Republic', N'DOP', N'NA', 9823821, 48730, N'Santo Domingo'),
(N'DZ', N'DZA', N'Algeria', N'DZD', N'AF', 34586184, 2381740, N'Algiers'),
(N'EC', N'ECU', N'Ecuador', N'USD', N'SA', 14790608, 283560, N'Quito'),
(N'EE', N'EST', N'Estonia', N'EUR', N'EU', 1291170, 45226, N'Tallinn'),
(N'EG', N'EGY', N'Egypt', N'EGP', N'AF', 80471869, 1001450, N'Cairo'),
(N'EH', N'ESH', N'Western Sahara', N'MAD', N'AF', 273008, 266000, N'El Aai?n'),
(N'ER', N'ERI', N'Eritrea', N'ERN', N'AF', 5792984, 121320, N'Asmara'),
(N'ES', N'ESP', N'Spain', N'EUR', N'EU', 46505963, 504782, N'Madrid'),
(N'ET', N'ETH', N'Ethiopia', N'ETB', N'AF', 88013491, 1127127, N'Addis Ababa'),
(N'FI', N'FIN', N'Finland', N'EUR', N'EU', 5244000, 337030, N'Helsinki'),
(N'FJ', N'FJI', N'Fiji', N'FJD', N'OC', 875983, 18270, N'Suva'),
(N'FK', N'FLK', N'Falkland Islands', N'FKP', N'SA', 2638, 12173, N'Stanley'),
(N'FM', N'FSM', N'Micronesia', N'USD', N'OC', 107708, 702, N'Palikir'),
(N'FO', N'FRO', N'Faroe Islands', N'DKK', N'EU', 48228, 1399, N'T?rshavn'),
(N'FR', N'FRA', N'France', N'EUR', N'EU', 64768389, 547030, N'Paris'),
(N'GA', N'GAB', N'Gabon', N'XAF', N'AF', 1545255, 267667, N'Libreville'),
(N'GB', N'GBR', N'United Kingdom', N'GBP', N'EU', 62348447, 244820, N'London'),
(N'GD', N'GRD', N'Grenada', N'XCD', N'NA', 107818, 344, N'St. George''s'),
(N'GE', N'GEO', N'Georgia', N'GEL', N'AS', 4630000, 69700, N'Tbilisi'),
(N'GF', N'GUF', N'French Guiana', N'EUR', N'SA', 195506, 91000, N'Cayenne'),
(N'GG', N'GGY', N'Guernsey', N'GBP', N'EU', 65228, 78, N'St Peter Port'),
(N'GH', N'GHA', N'Ghana', N'GHS', N'AF', 24339838, 239460, N'Accra'),
(N'GI', N'GIB', N'Gibraltar', N'GIP', N'EU', 27884, 6.5, N'Gibraltar'),
(N'GL', N'GRL', N'Greenland', N'DKK', N'NA', 56375, 2166086, N'Nuuk'),
(N'GM', N'GMB', N'Gambia', N'GMD', N'AF', 1593256, 11300, N'Banjul'),
(N'GN', N'GIN', N'Guinea', N'GNF', N'AF', 10324025, 245857, N'Conakry'),
(N'GP', N'GLP', N'Guadeloupe', N'EUR', N'NA', 443000, 1780, N'Basse-Terre'),
(N'GQ', N'GNQ', N'Equatorial Guinea', N'XAF', N'AF', 1014999, 28051, N'Malabo'),
(N'GR', N'GRC', N'Greece', N'EUR', N'EU', 11000000, 131940, N'Athens'),
(N'GS', N'SGS', N'South Georgia and the South Sandwich Islands', N'GBP', N'AN', 30, 3903, N'Grytviken'),
(N'GT', N'GTM', N'Guatemala', N'GTQ', N'NA', 13550440, 108890, N'Guatemala City'),
(N'GU', N'GUM', N'Guam', N'USD', N'OC', 159358, 549, N'Hag?t?a'),
(N'GW', N'GNB', N'Guinea-Bissau', N'XOF', N'AF', 1565126, 36120, N'Bissau'),
(N'GY', N'GUY', N'Guyana', N'GYD', N'SA', 748486, 214970, N'Georgetown'),
(N'HK', N'HKG', N'Hong Kong', N'HKD', N'AS', 6898686, 1092, N'Hong Kong'),
(N'HM', N'HMD', N'Heard Island and McDonald Islands', N'AUD', N'AN', 0, 412, N''),
(N'HN', N'HND', N'Honduras', N'HNL', N'NA', 7989415, 112090, N'Tegucigalpa'),
(N'HR', N'HRV', N'Croatia', N'HRK', N'EU', 4491000, 56542, N'Zagreb'),
(N'HT', N'HTI', N'Haiti', N'HTG', N'NA', 9648924, 27750, N'Port-au-Prince'),
(N'HU', N'HUN', N'Hungary', N'HUF', N'EU', 9982000, 93030, N'Budapest'),
(N'ID', N'IDN', N'Indonesia', N'IDR', N'AS', 242968342, 1919440, N'Jakarta'),
(N'IE', N'IRL', N'Ireland', N'EUR', N'EU', 4622917, 70280, N'Dublin'),
(N'IL', N'ISR', N'Israel', N'ILS', N'AS', 7353985, 20770, N''),
(N'IM', N'IMN', N'Isle of Man', N'GBP', N'EU', 75049, 572, N'Douglas'),
(N'IN', N'IND', N'India', N'INR', N'AS', 1173108018, 3287590, N'New Delhi'),
(N'IO', N'IOT', N'British Indian Ocean Territory', N'USD', N'AS', 4000, 60, N''),
(N'IQ', N'IRQ', N'Iraq', N'IQD', N'AS', 29671605, 437072, N'Baghdad'),
(N'IR', N'IRN', N'Iran', N'IRR', N'AS', 76923300, 1648000, N'Tehran'),
(N'IS', N'ISL', N'Iceland', N'ISK', N'EU', 308910, 103000, N'Reykjavik'),
(N'IT', N'ITA', N'Italy', N'EUR', N'EU', 60340328, 301230, N'Rome'),
(N'JE', N'JEY', N'Jersey', N'GBP', N'EU', 90812, 116, N'Saint Helier'),
(N'JM', N'JAM', N'Jamaica', N'JMD', N'NA', 2847232, 10991, N'Kingston'),
(N'JO', N'JOR', N'Jordan', N'JOD', N'AS', 6407085, 92300, N'Amman'),
(N'JP', N'JPN', N'Japan', N'JPY', N'AS', 127288000, 377835, N'Tokyo'),
(N'KE', N'KEN', N'Kenya', N'KES', N'AF', 40046566, 582650, N'Nairobi'),
(N'KG', N'KGZ', N'Kyrgyzstan', N'KGS', N'AS', 5508626, 198500, N'Bishkek'),
(N'KH', N'KHM', N'Cambodia', N'KHR', N'AS', 14453680, 181040, N'Phnom Penh'),
(N'KI', N'KIR', N'Kiribati', N'AUD', N'OC', 92533, 811, N'Tarawa'),
(N'KM', N'COM', N'Comoros', N'KMF', N'AF', 773407, 2170, N'Moroni'),
(N'KN', N'KNA', N'Saint Kitts and Nevis', N'XCD', N'NA', 51134, 261, N'Basseterre'),
(N'KP', N'PRK', N'North Korea', N'KPW', N'AS', 22912177, 120540, N'Pyongyang'),
(N'KR', N'KOR', N'South Korea', N'KRW', N'AS', 48422644, 98480, N'Seoul'),
(N'KW', N'KWT', N'Kuwait', N'KWD', N'AS', 2789132, 17820, N'Kuwait City'),
(N'KY', N'CYM', N'Cayman Islands', N'KYD', N'NA', 44270, 262, N'George Town'),
(N'KZ', N'KAZ', N'Kazakhstan', N'KZT', N'AS', 15340000, 2717300, N'Astana'),
(N'LA', N'LAO', N'Laos', N'LAK', N'AS', 6368162, 236800, N'Vientiane'),
(N'LB', N'LBN', N'Lebanon', N'LBP', N'AS', 4125247, 10400, N'Beirut'),
(N'LC', N'LCA', N'Saint Lucia', N'XCD', N'NA', 160922, 616, N'Castries'),
(N'LI', N'LIE', N'Liechtenstein', N'CHF', N'EU', 35000, 160, N'Vaduz'),
(N'LK', N'LKA', N'Sri Lanka', N'LKR', N'AS', 21513990, 65610, N'Colombo'),
(N'LR', N'LBR', N'Liberia', N'LRD', N'AF', 3685076, 111370, N'Monrovia'),
(N'LS', N'LSO', N'Lesotho', N'LSL', N'AF', 1919552, 30355, N'Maseru'),
(N'LT', N'LTU', N'Lithuania', N'EUR', N'EU', 2944459, 65200, N'Vilnius'),
(N'LU', N'LUX', N'Luxembourg', N'EUR', N'EU', 497538, 2586, N'Luxembourg'),
(N'LV', N'LVA', N'Latvia', N'EUR', N'EU', 2217969, 64589, N'Riga'),
(N'LY', N'LBY', N'Libya', N'LYD', N'AF', 6461454, 1759540, N'Tripoli'),
(N'MA', N'MAR', N'Morocco', N'MAD', N'AF', 31627428, 446550, N'Rabat'),
(N'MC', N'MCO', N'Monaco', N'EUR', N'EU', 32965, 1.95, N'Monaco'),
(N'MD', N'MDA', N'Moldova', N'MDL', N'EU', 4324000, 33843, N'Chisinau'),
(N'ME', N'MNE', N'Montenegro', N'EUR', N'EU', 666730, 14026, N'Podgorica'),
(N'MF', N'MAF', N'Saint Martin', N'EUR', N'NA', 35925, 53, N'Marigot'),
(N'MG', N'MDG', N'Madagascar', N'MGA', N'AF', 21281844, 587040, N'Antananarivo'),
(N'MH', N'MHL', N'Marshall Islands', N'USD', N'OC', 65859, 181.3, N'Majuro'),
(N'MK', N'MKD', N'Macedonia', N'MKD', N'EU', 2062294, 25333, N'Skopje'),
(N'ML', N'MLI', N'Mali', N'XOF', N'AF', 13796354, 1240000, N'Bamako'),
(N'MM', N'MMR', N'Myanmar', N'MMK', N'AS', 53414374, 678500, N'Nay Pyi Taw'),
(N'MN', N'MNG', N'Mongolia', N'MNT', N'AS', 3086918, 1565000, N'Ulan Bator'),
(N'MO', N'MAC', N'Macao', N'MOP', N'AS', 449198, 254, N'Macao'),
(N'MP', N'MNP', N'Northern Mariana Islands', N'USD', N'OC', 53883, 477, N'Saipan'),
(N'MQ', N'MTQ', N'Martinique', N'EUR', N'NA', 432900, 1100, N'Fort-de-France'),
(N'MR', N'MRT', N'Mauritania', N'MRO', N'AF', 3205060, 1030700, N'Nouakchott'),
(N'MS', N'MSR', N'Montserrat', N'XCD', N'NA', 9341, 102, N'Plymouth'),
(N'MT', N'MLT', N'Malta', N'EUR', N'EU', 403000, 316, N'Valletta'),
(N'MU', N'MUS', N'Mauritius', N'MUR', N'AF', 1294104, 2040, N'Port Louis'),
(N'MV', N'MDV', N'Maldives', N'MVR', N'AS', 395650, 300, N'Mal?'),
(N'MW', N'MWI', N'Malawi', N'MWK', N'AF', 15447500, 118480, N'Lilongwe'),
(N'MX', N'MEX', N'Mexico', N'MXN', N'NA', 112468855, 1972550, N'Mexico City'),
(N'MY', N'MYS', N'Malaysia', N'MYR', N'AS', 28274729, 329750, N'Kuala Lumpur'),
(N'MZ', N'MOZ', N'Mozambique', N'MZN', N'AF', 22061451, 801590, N'Maputo'),
(N'NA', N'NAM', N'Namibia', N'NAD', N'AF', 2128471, 825418, N'Windhoek'),
(N'NC', N'NCL', N'New Caledonia', N'XPF', N'OC', 216494, 19060, N'Noumea'),
(N'NE', N'NER', N'Niger', N'XOF', N'AF', 15878271, 1267000, N'Niamey'),
(N'NF', N'NFK', N'Norfolk Island', N'AUD', N'OC', 1828, 34.6, N'Kingston'),
(N'NG', N'NGA', N'Nigeria', N'NGN', N'AF', 154000000, 923768, N'Abuja'),
(N'NI', N'NIC', N'Nicaragua', N'NIO', N'NA', 5995928, 129494, N'Managua'),
(N'NL', N'NLD', N'Netherlands', N'EUR', N'EU', 16645000, 41526, N'Amsterdam'),
(N'NO', N'NOR', N'Norway', N'NOK', N'EU', 5009150, 324220, N'Oslo'),
(N'NP', N'NPL', N'Nepal', N'NPR', N'AS', 28951852, 140800, N'Kathmandu'),
(N'NR', N'NRU', N'Nauru', N'AUD', N'OC', 10065, 21, N''),
(N'NU', N'NIU', N'Niue', N'NZD', N'OC', 2166, 260, N'Alofi'),
(N'NZ', N'NZL', N'New Zealand', N'NZD', N'OC', 4252277, 268680, N'Wellington'),
(N'OM', N'OMN', N'Oman', N'OMR', N'AS', 2967717, 212460, N'Muscat'),
(N'PA', N'PAN', N'Panama', N'PAB', N'NA', 3410676, 78200, N'Panama City'),
(N'PE', N'PER', N'Peru', N'PEN', N'SA', 29907003, 1285220, N'Lima'),
(N'PF', N'PYF', N'French Polynesia', N'XPF', N'OC', 270485, 4167, N'Papeete'),
(N'PG', N'PNG', N'Papua New Guinea', N'PGK', N'OC', 6064515, 462840, N'Port Moresby'),
(N'PH', N'PHL', N'Philippines', N'PHP', N'AS', 99900177, 300000, N'Manila'),
(N'PK', N'PAK', N'Pakistan', N'PKR', N'AS', 184404791, 803940, N'Islamabad'),
(N'PL', N'POL', N'Poland', N'PLN', N'EU', 38500000, 312685, N'Warsaw'),
(N'PM', N'SPM', N'Saint Pierre and Miquelon', N'EUR', N'NA', 7012, 242, N'Saint-Pierre'),
(N'PN', N'PCN', N'Pitcairn Islands', N'NZD', N'OC', 46, 47, N'Adamstown'),
(N'PR', N'PRI', N'Puerto Rico', N'USD', N'NA', 3916632, 9104, N'San Juan'),
(N'PS', N'PSE', N'Palestine', N'ILS', N'AS', 3800000, 5970, N''),
(N'PT', N'PRT', N'Portugal', N'EUR', N'EU', 10676000, 92391, N'Lisbon'),
(N'PW', N'PLW', N'Palau', N'USD', N'OC', 19907, 458, N'Melekeok - Palau State Capital'),
(N'PY', N'PRY', N'Paraguay', N'PYG', N'SA', 6375830, 406750, N'Asunci?n'),
(N'QA', N'QAT', N'Qatar', N'QAR', N'AS', 840926, 11437, N'Doha'),
(N'RE', N'REU', N'R?union', N'EUR', N'AF', 776948, 2517, N'Saint-Denis'),
(N'RO', N'ROU', N'Romania', N'RON', N'EU', 21959278, 237500, N'Bucharest'),
(N'RS', N'SRB', N'Serbia', N'RSD', N'EU', 7344847, 88361, N'Belgrade'),
(N'RU', N'RUS', N'Russia', N'RUB', N'EU', 140702000, 17100000, N'Moscow'),
(N'RW', N'RWA', N'Rwanda', N'RWF', N'AF', 11055976, 26338, N'Kigali'),
(N'SA', N'SAU', N'Saudi Arabia', N'SAR', N'AS', 25731776, 1960582, N'Riyadh'),
(N'SB', N'SLB', N'Solomon Islands', N'SBD', N'OC', 559198, 28450, N'Honiara'),
(N'SC', N'SYC', N'Seychelles', N'SCR', N'AF', 88340, 455, N'Victoria'),
(N'SD', N'SDN', N'Sudan', N'SDG', N'AF', 35000000, 1861484, N'Khartoum'),
(N'SE', N'SWE', N'Sweden', N'SEK', N'EU', 9555893, 449964, N'Stockholm'),
(N'SG', N'SGP', N'Singapore', N'SGD', N'AS', 4701069, 692.7, N'Singapore'),
(N'SH', N'SHN', N'Saint Helena', N'SHP', N'AF', 7460, 410, N'Jamestown'),
(N'SI', N'SVN', N'Slovenia', N'EUR', N'EU', 2007000, 20273, N'Ljubljana'),
(N'SJ', N'SJM', N'Svalbard and Jan Mayen', N'NOK', N'EU', 2550, 62049, N'Longyearbyen'),
(N'SK', N'SVK', N'Slovakia', N'EUR', N'EU', 5455000, 48845, N'Bratislava'),
(N'SL', N'SLE', N'Sierra Leone', N'SLL', N'AF', 5245695, 71740, N'Freetown'),
(N'SM', N'SMR', N'San Marino', N'EUR', N'EU', 31477, 61.2, N'San Marino'),
(N'SN', N'SEN', N'Senegal', N'XOF', N'AF', 12323252, 196190, N'Dakar'),
(N'SO', N'SOM', N'Somalia', N'SOS', N'AF', 10112453, 637657, N'Mogadishu'),
(N'SR', N'SUR', N'Suriname', N'SRD', N'SA', 492829, 163270, N'Paramaribo'),
(N'SS', N'SSD', N'South Sudan', N'SSP', N'AF', 8260490, 644329, N'Juba'),
(N'ST', N'STP', N'S?o Tom? and Pr?ncipe', N'STD', N'AF', 175808, 1001, N'S?o Tom?'),
(N'SV', N'SLV', N'El Salvador', N'USD', N'NA', 6052064, 21040, N'San Salvador'),
(N'SX', N'SXM', N'Sint Maarten', N'ANG', N'NA', 37429, 21, N'Philipsburg'),
(N'SY', N'SYR', N'Syria', N'SYP', N'AS', 22198110, 185180, N'Damascus'),
(N'SZ', N'SWZ', N'Swaziland', N'SZL', N'AF', 1354051, 17363, N'Mbabane'),
(N'TC', N'TCA', N'Turks and Caicos Islands', N'USD', N'NA', 20556, 430, N'Cockburn Town'),
(N'TD', N'TCD', N'Chad', N'XAF', N'AF', 10543464, 1284000, N'N''Djamena'),
(N'TF', N'ATF', N'French Southern Territories', N'EUR', N'AN', 140, 7829, N'Port-aux-Fran?ais'),
(N'TG', N'TGO', N'Togo', N'XOF', N'AF', 6587239, 56785, N'Lom?'),
(N'TH', N'THA', N'Thailand', N'THB', N'AS', 67089500, 514000, N'Bangkok'),
(N'TJ', N'TJK', N'Tajikistan', N'TJS', N'AS', 7487489, 143100, N'Dushanbe'),
(N'TK', N'TKL', N'Tokelau', N'NZD', N'OC', 1466, 10, N''),
(N'TL', N'TLS', N'East Timor', N'USD', N'OC', 1154625, 15007, N'Dili'),
(N'TM', N'TKM', N'Turkmenistan', N'TMT', N'AS', 4940916, 488100, N'Ashgabat'),
(N'TN', N'TUN', N'Tunisia', N'TND', N'AF', 10589025, 163610, N'Tunis'),
(N'TO', N'TON', N'Tonga', N'TOP', N'OC', 122580, 748, N'Nuku''alofa'),
(N'TR', N'TUR', N'Turkey', N'TRY', N'AS', 77804122, 780580, N'Ankara'),
(N'TT', N'TTO', N'Trinidad and Tobago', N'TTD', N'NA', 1228691, 5128, N'Port of Spain'),
(N'TV', N'TUV', N'Tuvalu', N'AUD', N'OC', 10472, 26, N'Funafuti'),
(N'TW', N'TWN', N'Taiwan', N'TWD', N'AS', 22894384, 35980, N'Taipei'),
(N'TZ', N'TZA', N'Tanzania', N'TZS', N'AF', 41892895, 945087, N'Dodoma'),
(N'UA', N'UKR', N'Ukraine', N'UAH', N'EU', 45415596, 603700, N'Kyiv'),
(N'UG', N'UGA', N'Uganda', N'UGX', N'AF', 33398682, 236040, N'Kampala'),
(N'UM', N'UMI', N'U.S. Minor Outlying Islands', N'USD', N'OC', 0, 0, N''),
(N'US', N'USA', N'United States', N'USD', N'NA', 310232863, 9629091, N'Washington'),
(N'UY', N'URY', N'Uruguay', N'UYU', N'SA', 3477000, 176220, N'Montevideo'),
(N'UZ', N'UZB', N'Uzbekistan', N'UZS', N'AS', 27865738, 447400, N'Tashkent'),
(N'VA', N'VAT', N'Vatican City', N'EUR', N'EU', 921, 0.44, N'Vatican'),
(N'VC', N'VCT', N'Saint Vincent and the Grenadines', N'XCD', N'NA', 104217, 389, N'Kingstown'),
(N'VE', N'VEN', N'Venezuela', N'VEF', N'SA', 27223228, 912050, N'Caracas'),
(N'VG', N'VGB', N'British Virgin Islands', N'USD', N'NA', 21730, 153, N'Road Town'),
(N'VI', N'VIR', N'U.S. Virgin Islands', N'USD', N'NA', 108708, 352, N'Charlotte Amalie'),
(N'VN', N'VNM', N'Vietnam', N'VND', N'AS', 89571130, 329560, N'Hanoi'),
(N'VU', N'VUT', N'Vanuatu', N'VUV', N'OC', 221552, 12200, N'Port Vila'),
(N'WF', N'WLF', N'Wallis and Futuna', N'XPF', N'OC', 16025, 274, N'Mata-Utu'),
(N'WS', N'WSM', N'Samoa', N'WST', N'OC', 192001, 2944, N'Apia'),
(N'XK', N'XKX', N'Kosovo', N'EUR', N'EU', 1800000, 10908, N'Pristina'),
(N'YE', N'YEM', N'Yemen', N'YER', N'AS', 23495361, 527970, N'Sanaa'),
(N'YT', N'MYT', N'Mayotte', N'EUR', N'AF', 159042, 374, N'Mamoutzou'),
(N'ZA', N'ZAF', N'South Africa', N'ZAR', N'AF', 49000000, 1219912, N'Pretoria'),
(N'ZM', N'ZMB', N'Zambia', N'ZMW', N'AF', 13460305, 752614, N'Lusaka'),
(N'ZW', N'ZWE', N'Zimbabwe', N'ZWD', N'AF', 11651858, 390580, N'Harare')

INSERT Currencies (CurrencyCode, Description) VALUES
(N'AED', N'United Arab Emirates Dirham'),
(N'AFN', N'Afghanistan Afghani'),
(N'ALL', N'Albania Lek'),
(N'AMD', N'Armenia Dram'),
(N'ANG', N'Netherlands Antilles Guilder'),
(N'AOA', N'Angola Kwanza'),
(N'ARS', N'Argentina Peso'),
(N'AUD', N'Australia Dollar'),
(N'AWG', N'Aruba Guilder'),
(N'AZN', N'Azerbaijan New Manat'),
(N'BAM', N'Bosnia and Herzegovina Convertible Marka'),
(N'BBD', N'Barbados Dollar'),
(N'BDT', N'Bangladesh Taka'),
(N'BGN', N'Bulgaria Lev'),
(N'BHD', N'Bahrain Dinar'),
(N'BIF', N'Burundi Franc'),
(N'BMD', N'Bermuda Dollar'),
(N'BND', N'Brunei Darussalam Dollar'),
(N'BOB', N'Bolivia Boliviano'),
(N'BRL', N'Brazil Real'),
(N'BSD', N'Bahamas Dollar'),
(N'BTN', N'Bhutan Ngultrum'),
(N'BWP', N'Botswana Pula'),
(N'BYR', N'Belarus Ruble'),
(N'BZD', N'Belize Dollar'),
(N'CAD', N'Canada Dollar'),
(N'CDF', N'Congo/Kinshasa Franc'),
(N'CHF', N'Switzerland Franc'),
(N'CLP', N'Chile Peso'),
(N'CNY', N'China Yuan Renminbi'),
(N'COP', N'Colombia Peso'),
(N'CRC', N'Costa Rica Colon'),
(N'CUC', N'Cuba Convertible Peso'),
(N'CUP', N'Cuba Peso'),
(N'CVE', N'Cape Verde Escudo'),
(N'CZK', N'Czech Republic Koruna'),
(N'DJF', N'Djibouti Franc'),
(N'DKK', N'Denmark Krone'),
(N'DOP', N'Dominican Republic Peso'),
(N'DZD', N'Algeria Dinar'),
(N'EGP', N'Egypt Pound'),
(N'ERN', N'Eritrea Nakfa'),
(N'ETB', N'Ethiopia Birr'),
(N'EUR', N'Euro Member Countries'),
(N'FJD', N'Fiji Dollar'),
(N'FKP', N'Falkland Islands (Malvinas) Pound'),
(N'GBP', N'United Kingdom Pound'),
(N'GEL', N'Georgia Lari'),
(N'GGP', N'Guernsey Pound'),
(N'GHS', N'Ghana Cedi'),
(N'GIP', N'Gibraltar Pound'),
(N'GMD', N'Gambia Dalasi'),
(N'GNF', N'Guinea Franc'),
(N'GTQ', N'Guatemala Quetzal'),
(N'GYD', N'Guyana Dollar'),
(N'HKD', N'Hong Kong Dollar'),
(N'HNL', N'Honduras Lempira'),
(N'HRK', N'Croatia Kuna'),
(N'HTG', N'Haiti Gourde'),
(N'HUF', N'Hungary Forint'),
(N'IDR', N'Indonesia Rupiah'),
(N'ILS', N'Israel Shekel'),
(N'IMP', N'Isle of Man Pound'),
(N'INR', N'India Rupee'),
(N'IQD', N'Iraq Dinar'),
(N'IRR', N'Iran Rial'),
(N'ISK', N'Iceland Krona'),
(N'JEP', N'Jersey Pound'),
(N'JMD', N'Jamaica Dollar'),
(N'JOD', N'Jordan Dinar'),
(N'JPY', N'Japan Yen'),
(N'KES', N'Kenya Shilling'),
(N'KGS', N'Kyrgyzstan Som'),
(N'KHR', N'Cambodia Riel'),
(N'KMF', N'Comoros Franc'),
(N'KPW', N'Korea (North) Won'),
(N'KRW', N'Korea (South) Won'),
(N'KWD', N'Kuwait Dinar'),
(N'KYD', N'Cayman Islands Dollar'),
(N'KZT', N'Kazakhstan Tenge'),
(N'LAK', N'Laos Kip'),
(N'LBP', N'Lebanon Pound'),
(N'LKR', N'Sri Lanka Rupee'),
(N'LRD', N'Liberia Dollar'),
(N'LSL', N'Lesotho Loti'),
(N'LYD', N'Libya Dinar'),
(N'MAD', N'Morocco Dirham'),
(N'MDL', N'Moldova Leu'),
(N'MGA', N'Madagascar Ariary'),
(N'MKD', N'Macedonia Denar'),
(N'MMK', N'Myanmar (Burma) Kyat'),
(N'MNT', N'Mongolia Tughrik'),
(N'MOP', N'Macau Pataca'),
(N'MRO', N'Mauritania Ouguiya'),
(N'MUR', N'Mauritius Rupee'),
(N'MVR', N'Maldives (Maldive Islands) Rufiyaa'),
(N'MWK', N'Malawi Kwacha'),
(N'MXN', N'Mexico Peso'),
(N'MYR', N'Malaysia Ringgit'),
(N'MZN', N'Mozambique Metical'),
(N'NAD', N'Namibia Dollar'),
(N'NGN', N'Nigeria Naira'),
(N'NIO', N'Nicaragua Cordoba'),
(N'NOK', N'Norway Krone'),
(N'NPR', N'Nepal Rupee'),
(N'NZD', N'New Zealand Dollar'),
(N'OMR', N'Oman Rial'),
(N'PAB', N'Panama Balboa'),
(N'PEN', N'Peru Nuevo Sol'),
(N'PGK', N'Papua New Guinea Kina'),
(N'PHP', N'Philippines Peso'),
(N'PKR', N'Pakistan Rupee'),
(N'PLN', N'Poland Zloty'),
(N'PYG', N'Paraguay Guarani'),
(N'QAR', N'Qatar Riyal'),
(N'RON', N'Romania New Leu'),
(N'RSD', N'Serbia Dinar'),
(N'RUB', N'Russia Ruble'),
(N'RWF', N'Rwanda Franc'),
(N'SAR', N'Saudi Arabia Riyal'),
(N'SBD', N'Solomon Islands Dollar'),
(N'SCR', N'Seychelles Rupee'),
(N'SDG', N'Sudan Pound'),
(N'SEK', N'Sweden Krona'),
(N'SGD', N'Singapore Dollar'),
(N'SHP', N'Saint Helena Pound'),
(N'SLL', N'Sierra Leone Leone'),
(N'SOS', N'Somalia Shilling'),
(N'SPL', N'Seborga Luigino'),
(N'SRD', N'Suriname Dollar'),
(N'SSP', N'South Sudanese Pound'),
(N'STD', N'S?o Tom? and Pr?ncipe Dobra'),
(N'SVC', N'El Salvador Colon'),
(N'SYP', N'Syria Pound'),
(N'SZL', N'Swaziland Lilangeni'),
(N'THB', N'Thailand Baht'),
(N'TJS', N'Tajikistan Somoni'),
(N'TMT', N'Turkmenistan Manat'),
(N'TND', N'Tunisia Dinar'),
(N'TOP', N'Tonga Pa''anga'),
(N'TRY', N'Turkey Lira'),
(N'TTD', N'Trinidad and Tobago Dollar'),
(N'TVD', N'Tuvalu Dollar'),
(N'TWD', N'Taiwan New Dollar'),
(N'TZS', N'Tanzania Shilling'),
(N'UAH', N'Ukraine Hryvnia'),
(N'UGX', N'Uganda Shilling'),
(N'USD', N'United States Dollar'),
(N'UYU', N'Uruguay Peso'),
(N'UZS', N'Uzbekistan Som'),
(N'VEF', N'Venezuela Bolivar'),
(N'VND', N'Viet Nam Dong'),
(N'VUV', N'Vanuatu Vatu'),
(N'WST', N'Samoa Tala'),
(N'XAF', N'Communaut? Financi?re Africaine (BEAC) CFA Franc BEAC'),
(N'XCD', N'East Caribbean Dollar'),
(N'XDR', N'International Monetary Fund (IMF) Special Drawing Rights'),
(N'XOF', N'Communaut? Financi?re Africaine (BCEAO) Franc'),
(N'XPF', N'Comptoirs Fran?ais du Pacifique (CFP) Franc'),
(N'YER', N'Yemen Rial'),
(N'ZAR', N'South Africa Rand'),
(N'ZMW', N'Zambia Kwacha'),
(N'ZWD', N'Zimbabwe Dollar')

SET IDENTITY_INSERT Mountains ON
INSERT Mountains (Id, MountainRange) VALUES
(1, N'Alaska Range'),
(2, N'Alborz'),
(3, N'Andes'),
(4, N'Balkan Mountains'),
(5, N'Caucasus'),
(6, N'Cordillera Neovolcanica'),
(7, N'Ellsworth Mountains'),
(8, N'Executive Committee Range'),
(9, N'Himalayas'),
(10, N'Jayawijaya Mountains'),
(11, N'Karakoram'),
(12, N'Kenyan Highlands'),
(13, N'Kilimanjaro'),
(14, N'Kilimanjaro Region'),
(15, N'Maoke Mountains'),
(16, N'Pirin'),
(17, N'Rila'),
(18, N'Saint Elias Mountains'),
(19, N'Sentinel Range'),
(20, N'Southern Highlands'),
(21, N'The Sudirman Range'),
(22, N'Trans-Mexican Volcanic Belt'),
(23, N'Rhodope Mountains'),
(24, N'Vitosha'),
(25, N'Strandza'),
(26, N'Monte Rosa')
SET IDENTITY_INSERT Mountains OFF

INSERT MountainsCountries (MountainId, CountryCode) VALUES
(1, N'US'),
(2, N'IR'),
(3, N'AR'),
(3, N'CL'),
(4, N'BG'),
(5, N'GE'),
(5, N'RU'),
(6, N'MX'),
(9, N'CN'),
(9, N'IN'),
(9, N'NP'),
(10, N'ID'),
(11, N'CN'),
(11, N'PK'),
(12, N'KE'),
(13, N'TZ'),
(14, N'TZ'),
(15, N'ID'),
(16, N'BG'),
(17, N'BG'),
(18, N'CA'),
(20, N'PG'),
(21, N'ID'),
(22, N'MX'),
(23, N'BG'),
(24, N'BG'),
(25, N'BG'),
(26, N'IT'),
(26, N'CH')

SET IDENTITY_INSERT Peaks ON
INSERT Peaks (Id, PeakName, Elevation, MountainId) VALUES
(62, N'Aconcagua', 6962, 3),
(63, N'Botev', 2376, 4),
(64, N'Carstensz Pyramid', 4884, 21),
(65, N'Damavand', 5610, 2),
(66, N'Dykh-Tau', 5205, 5),
(67, N'Elbrus', 5642, 5),
(68, N'Everest', 8848, 9),
(69, N'Julianatop', 4760, 10),
(70, N'K2', 8611, 11),
(71, N'Kangchenjunga', 8586, 9),
(72, N'Kilimanjaro', 5895, 13),
(73, N'Malyovitsa', 2729, 17),
(74, N'Mawenzi', 5149, 14),
(75, N'Monte Pissis', 6793, 3),
(76, N'Mount Giluwe', 4368, 20),
(77, N'Mount Kenya', 5199, 12),
(78, N'Mount Logan', 5959, 18),
(79, N'Mount McKinley', 6194, 1),
(80, N'Mount Shinn', 4661, 19),
(81, N'Mount Sidley', 4285, 8),
(82, N'Mount Tyree', 4852, 19),
(83, N'Musala', 2925, 17),
(84, N'Ojos del Salado', 6893, 3),
(85, N'Pico de Orizaba', 5636, 22),
(86, N'Puncak Trikora', 4750, 15),
(87, N'Shkhara', 5193, 5),
(88, N'Vihren', 2914, 16),
(89, N'Vinson Massif', 4897, 7),
(90, N'Golyam Perelik', 2191, 23),
(91, N'Shirokolashki Snezhnik', 2188, 23),
(92, N'Golyam Persenk', 2091, 23),
(93, N'Batashki Snezhnik', 2082, 23),
(94, N'Cerro Bonete', 6759, 3),
(95, N'Gal?n', 5912, 3),
(96, N'Mercedario', 6720, 3),
(97, N'Pissis', 6795, 3),
(98, N'Lhotse', 8516, 9),
(99, N'Makalu', 8462, 9),
(100, N'Cho Oyu', 8201, 9),
(101, N'Kutelo', 2908, 16),
(102, N'Banski Suhodol', 2884, 16),
(103, N'Golyam Polezhan', 2851, 16),
(104, N'Kamenitsa', 2822, 16),
(105, N'Malak Polezhan', 2822, 16),
(106, N'Malka Musala', 2902, 17),
(107, N'Orlovets', 2685, 17),
(108, N'Vezhen', 2198, 4),
(109, N'Kom', 2016, 4)
SET IDENTITY_INSERT Peaks OFF

SET IDENTITY_INSERT Rivers ON 
INSERT Rivers (Id, RiverName, Length, DrainageArea, AverageDischarge, Outflow) VALUES
(1, N'Nile', 6650, 3254555, 5100, N'Mediterranean'),
(2, N'Amazon', 6400, 7050000, 219000, N'Atlantic Ocean'),
(3, N'Yangtze', 6300, 1800000, 31900, N'East China Sea'),
(4, N'Mississippi', 6275, 2980000, 16200, N'Gulf of Mexico'),
(5, N'Yenisei', 5539, 2580000, 19600, N'Kara Sea'),
(6, N'Yellow River', 5464, 745000, 2110, N'Bohai Sea'),
(7, N'Ob', 5410, 2990000, 12800, N'Gulf of Ob'),
(8, N'Paran?', 4880, 2582672, 18000, N'R?o de la Plata'),
(9, N'Congo', 4700, 3680000, 41800, N'Atlantic Ocean'),
(10, N'Amur', 4444, 1855000, 11400, N'Sea of Okhotsk'),
(11, N'Lena', 4400, 2490000, 17100, N'Laptev Sea'),
(12, N'Mekong', 4350, 810000, 16000, N'South China Sea'),
(13, N'Mackenzie', 4241, 1790000, 10300, N'Beaufort Sea'),
(14, N'Niger', 4200, 2090000, 9570, N'Gulf of Guinea'),
(15, N'Murray', 3672, 1061000, 7670, N'Southern Ocean'),
(16, N'Tocantins', 3650, 950000, 13598, N'Atlantic Ocean, Amazon'),
(17, N'Volga', 3645, 1380000, 8080, N'Caspian Sea'),
(18, N'Shatt al-Arab', 3596, 884000, 8560, N'Persian Gulf'),
(19, N'Madeira', 3380, 1485200, 31200, N'Amazon'),
(20, N'Pur?s', 3211, 63166, 8400, N'Amazon'),
(21, N'Yukon', 3185, 850000, 6210, N'Bering Sea'),
(22, N'Indus', 3180, 960000, 7160, N'Arabian Sea'),
(23, N'S?o Francisco', 3180, 610000, 3300, N'Atlantic Ocean'),
(24, N'Syr Darya', 3078, 219000, 7030, N'Aral Sea'),
(25, N'Salween', 3060, 324000, 3153, N'Andaman Sea'),
(26, N'Saint Lawrence', 3058, 1030000, 10100, N'Gulf of Saint Lawrence'),
(27, N'Rio Grande', 3057, 570000, 820, N'Gulf of Mexico'),
(28, N'Lower Tunguska', 2989, 473000, 3600, N'Yenisei'),
(29, N'Brahmaputra', 2948, 1730000, 19200, N'Ganges'),
(30, N'Danube', 2888, 817000, 7130, N'Black Sea')
SET IDENTITY_INSERT Rivers OFF

INSERT CountriesRivers (RiverId, CountryCode) VALUES
(9, N'AO'),
(8, N'AR'),
(30, N'AT'),
(15, N'AU'),
(29, N'BD'),
(14, N'BF'),
(30, N'BG'),
(1, N'BI'),
(9, N'BI'),
(14, N'BJ'),
(2, N'BO'),
(8, N'BO'),
(19, N'BO'),
(2, N'BR'),
(8, N'BR'),
(16, N'BR'),
(19, N'BR'),
(20, N'BR'),
(23, N'BR'),
(29, N'BT'),
(4, N'CA'),
(13, N'CA'),
(21, N'CA'),
(26, N'CA'),
(1, N'CD'),
(9, N'CD'),
(9, N'CF'),
(9, N'CG'),
(9, N'CM'),
(14, N'CM'),
(3, N'CN'),
(6, N'CN'),
(7, N'CN'),
(10, N'CN'),
(12, N'CN'),
(22, N'CN'),
(25, N'CN'),
(29, N'CN'),
(2, N'CO'),
(30, N'DE'),
(14, N'DZ'),
(2, N'EC'),
(1, N'EG'),
(1, N'ER'),
(1, N'ET'),
(14, N'GN'),
(2, N'GY'),
(30, N'HR'),
(30, N'HU'),
(22, N'IN'),
(29, N'IN'),
(18, N'IQ'),
(1, N'KE'),
(24, N'KG'),
(12, N'KH'),
(7, N'KZ'),
(24, N'KZ'),
(12, N'LA'),
(14, N'ML'),
(12, N'MM'),
(25, N'MM'),
(5, N'MN'),
(7, N'MN'),
(10, N'MN'),
(27, N'MX'),
(14, N'NE'),
(14, N'NG'),
(29, N'NP'),
(2, N'PE'),
(19, N'PE'),
(20, N'PE'),
(22, N'PK'),
(8, N'PY'),
(30, N'RO'),
(30, N'RS'),
(5, N'RU'),
(7, N'RU'),
(10, N'RU'),
(11, N'RU'),
(17, N'RU'),
(28, N'RU'),
(1, N'RW'),
(9, N'RW'),
(1, N'SD'),
(30, N'SK'),
(1, N'SS'),
(18, N'SY'),
(14, N'TD'),
(12, N'TH'),
(25, N'TH'),
(24, N'TJ'),
(18, N'TR'),
(1, N'TZ'),
(9, N'TZ'),
(1, N'UG'),
(4, N'US'),
(21, N'US'),
(26, N'US'),
(27, N'US'),
(8, N'UY'),
(24, N'UZ'),
(2, N'VE'),
(12, N'VN'),
(9, N'ZM')

GO

-- Add integrity constraints
ALTER TABLE Countries WITH CHECK ADD CONSTRAINT FK_Countries_Continents
FOREIGN KEY(ContinentCode) REFERENCES Continents (ContinentCode)
GO

ALTER TABLE Countries WITH CHECK ADD CONSTRAINT FK_Countries_Currencies
FOREIGN KEY(CurrencyCode) REFERENCES Currencies (CurrencyCode)
GO

ALTER TABLE CountriesRivers WITH CHECK ADD CONSTRAINT FK_CountriesRivers_Countries
FOREIGN KEY(CountryCode) REFERENCES Countries (CountryCode)
GO

ALTER TABLE CountriesRivers WITH CHECK ADD CONSTRAINT FK_CountriesRivers_Rivers
FOREIGN KEY(RiverId) REFERENCES Rivers (Id)
GO

ALTER TABLE MountainsCountries WITH CHECK ADD CONSTRAINT FK_MountainsCountries_Countries
FOREIGN KEY(CountryCode) REFERENCES Countries (CountryCode)
GO

ALTER TABLE MountainsCountries WITH CHECK ADD CONSTRAINT FK_MountainsCountries_Mountains
FOREIGN KEY(MountainId) REFERENCES Mountains (Id)
GO

ALTER TABLE Peaks WITH CHECK ADD CONSTRAINT FK_Peaks_Mountains
FOREIGN KEY(MountainId) REFERENCES Mountains (Id)
GO
