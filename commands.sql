CREATE DATABASE IF NOT EXISTS streaks_db;

CREATE TABLE IF NOT EXISTS streak(
	StreakId UUID PRIMARY KEY,
	StreakName VARCHAR(50) NOT NULL,
	StreakDescription text NULL,
	LongestStreak Integer DEFAULT 0 NOT NULL
);

CREATE TABLE IF NOT EXISTS streak_day(
    Id DATE  NOT NULL DEFAULT CURRENT_DATE,
    Complete BOOLEAN NOT NULL DEFAULT FALSE,
    CONSTRAINT streak
        FOREIGN KEY(StreakId)
            REFERENCES streak(StreakId)
    PRIMARY KEY (StreakId, Id)
);

INSERT INTO streak(streakid, streakname, streakdescription, longeststreak) 
	VALUES('8e85bdae-0775-11ed-b939-0242ac120002', 'Meditation', 'Its worth it', 0);