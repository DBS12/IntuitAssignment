# Dor Ben Shimon Intuit Assignment

## Overview

This document outlines the implementation details of the Intuit Assignment, including basic architecture, trade-offs, open questions, scalability considerations, and potential improvements.

## Basic Architecture

### Engine

- **Background Worker:**
  - Upon service startup, a background worker reads CSV files record by record.
  - Records are gathered in bulks of 1,000 and inserted into the database.
  - This process runs continuously in the background.

- **CSV Reading:**
  - Uses the `CsvReader` NuGet package.
  - Exception handling includes:
    - **DB Unavailability:** Retries insertion upon failure; aborts reading if retries fail.
    - **Invalid Records:** Skips invalid records.

- **Scalability:**
  - For very large files (gigabytes to petabytes):
    - **Custom CsvReader:** Implementing a custom `CsvReader` with multiple background workers.
    - **Worker Offsets:** Divide data into offsets for parallel processing (e.g., 500 million rows divided among workers).
    - **Future Improvements:** This feature has not been implemented but is a potential area for scaling improvements.

### Database

- **In-Memory DB:**
  - Uses a `Dictionary/Map` + LRU cache to support scalable retrieval
  - Maintains a `List` of players for pagination.
  - Designed to handle heavy read operations, simulating a real database.
  
- **Design:**
  - Implements the `IPlayerDAL` interface.
  - Uses dependency injection for easy replacement with other data access layers (e.g., MySQL or Redis).
  - **Retry Mechanism:** Includes retry logic for data insertion failures (e.g., database downtime, network issues).

### Scrapers

- **Implemented Scrapers:**
  - **baseball-reference.com**
  - **retrosheet.org dataset**

- **Functionality:**
  - Fetches metadata from these sites using UUIDs (retroID/bbrefID).
  - Originally considered fetching data 

### API

- **Endpoints:**
  - **GetPlayerByID(string ID):** Fetches data from the DAL and runs scrapers if necessary. Prioritizes LRU cache lookup (O(1)) before querying the database.
  - **GetAllPlayers(int limit, int page):** Provides pagination for retrieving player data. It is not feasible to return all data at once, so a pagination mechanism is used.

## Trade-Offs

- **In-Memory DB vs. Persistent Store:** Chose in-memory DB for simplicity and speed, but a persistent store (e.g., MySQL, Redis) could be used for more extensive datasets and durability.
- **Scraper Updates:** Instead of fetching data in real-time, scrapers fetch metadata on demand, which ensures data consistency.

## Future Improvements

- **Custom CsvReader:** Develop a custom CSV reader to enhance performance and scalability for very large files.
