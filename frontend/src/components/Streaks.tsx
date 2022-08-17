import React from "react";
import Streak from "./Streak";

import { useAppSelector } from "../app/hooks";

const Streaks = () => {
  const streaks = useAppSelector((state) => state.streaks);

  return (
    <div className="col-span-3 max-w-[500px]  sm:mx-10 sm:grid-cols-2 xl:ml-30 xl:gap-y-4 xl: gap-8 lg:mt-0 mt-10  sm:grid   flex flex-col ">
      {streaks.map((streak) => (
        <Streak key={streak.streakId} streak={streak} />
      ))}
    </div>
  );
};

export default Streaks;
