import React from "react";
import Streak from "./Streak";

import { useAppSelector } from "../app/hooks";

const Streaks = () => {
  const streaks = useAppSelector((state) => state.streaks);

  return (
    <div className="col-span-3 grid lg:grid-cols-2 sm:grid-cols-1 md:ml-20 md:gap-10 gap-4">
      {streaks.map((streak) => (
        <Streak key={streak.streakId} streak={streak} />
      ))}
    </div>
  );
};

export default Streaks;
