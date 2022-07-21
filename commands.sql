CREATE DATABASE IF NOT EXISTS streaks;

CREATE TABLE IF NOT EXISTS streak(
	StreakId UUID PRIMARY KEY,
	StreakName VARCHAR(50) NOT NULL,
	StreakDescription text NULL,
	LongestStreak Integer DEFAULT 0 NOT NULL
);

CREATE TABLE IF NOT EXISTS streak_day(
    Id DATE  NOT NULL DEFAULT CURRENT_DATE::DATE,
    Complete BOOLEAN NOT NULL DEFAULT FALSE,
    CONSTRAINT streak
        FOREIGN KEY(StreakId)
            REFERENCES streak(StreakId)
    PRIMARY KEY (StreakId, Id)
);


INSERT INTO streak(streakid, streakname, streakdescription, longeststreak) 
	VALUES('8e85bdae-0775-11ed-b939-0242ac120002', 'Meditation', 'Its worth it', 0);

UPDATE streak
SET streakname = 'Japanese', streakdescription = 'Please don''t abandon me'
WHERE streakid = '24ffa56f-9f7a-42c5-a4f6-1ba0c4a405cd';

DELETE FROM streak WHERE streakid = 'f7185f03-fe4f-4443-ba04-4777d99dce74';

INSERT INTO streak_day(id, complete, streakid) VALUES ('2022-07-22', TRUE ,'6de46eb7-3812-47a3-bbb6-7c7c54ef922b');

UPDATE streak_day
SET complete = FALSE
WHERE Id = '2022-06-20' AND streakid::text = '6de46eb7-3812-47a3-bbb6-7c7c54ef922b';

-- QUERY TO GET CURRENT STREAK
SELECT 
  * 
FROM 
  (
    SELECT 
      Count(*) AS Streak, 
      DATE(streak) -1 AS CurrDay 
    FROM 
      (
        SELECT 
          Id, 
          Id + ROW_NUMBER() OVER(
            ORDER BY 
              Id DESC
          ) * interval '1 day' AS Streak 
        FROM 
          streak_day 
        WHERE 
          streakid = '6de46eb7-3812-47a3-bbb6-7c7c54ef922b'
      ) AS getconsecutedates 
    GROUP BY 
      streak
  ) as groupbydates 
WHERE 
  (
    CurrDay = CURRENT_DATE 
    OR CurrDay = CURRENT_DATE - 1
  ) 
ORDER BY 
  CurrDay DESC 
LIMIT 
  1;
