Dor Ben Shimon Intuit Assignment

I will elaborate on what I did, trade-offs, open questions, scalability, and how to improve.

Basic Architecture -

Engine - 
	Once the service start, we are running a background worker that read the CSV files record by record
	We are gathering up bulks of 1000 records, and inserting them to the DB.
	This process is running in the background.
	Our engine is using CsvReader nuget
	Our engine has exception handling for:
		* DB is not available (insertion failed with retries - aborting the read)
		* Record not valid - skipping record
	In terms of scalability - In case the file is very large (Giga/Peta bytes) - I had an idea to create my own CsvReader - which will work with multiple background workers, and with offsets for example:
		- 500Million rows of data in the CSV
		- Create multiple background workers (can be configurable)
		- Each background worked will work on different offsets of data (0 - 100m - worker#1 , 100,000,001 - 500m - worker#2 etc ...) this will ease and fasten the read
		- I haven't implement such thing here but this is a possibility to improve scale
	Tests: [V]

Database -
	* I chose to create an in memory DB:
		Dictionary/Map (to fetch player with O(1))) - can be replaced with Redis for example (serves GetPlayerByID)
		List of players in order to support pagination when trying to GetAllPlayers - I have added LRU cache to this method to bring the popular players and be even more scale able
	* The DB inherit IPlayerDAL, and injected with dependecy injection through the service, what makes it easy to replace the DAL with MySql/Redis etc.. (I'd choose MySql here in order to easily support pagination althought we can have SortedList in redis which can help achive it as well)
	* Our DB has retry mechanism which retry to insert data in case of failure (DB is down, any other network issue)

Scrappers -
	* I have created 2 scrapers for:
		baseball-reference.com
		retrosheet.org dataset
		These scappers fetching meta data from these sites upon given UUID (retroID/bbrefID) - I considered fetching the data at the write time (in the background worker) but than thought it might be not updated.
	*** To be honest - I used lot of help from GPT writing the scrapers here

API - 
	* As requested, 2 APIs -
		* GetPlayerByID(string ID) - will fetch the data from the DAL + run the scrapers - Will prioritize LRU cache lookup (O(1)), and than will go to DB
		* GetAllPlayers(int limit, int page) - it is impossible to return all of the data it self, so I exposed pagination mechanism with this query
