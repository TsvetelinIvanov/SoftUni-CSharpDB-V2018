ALTER TABLE Users
ADD CONSTRAINT ch_Password CHECK(LEN([Password]) >= 5)

