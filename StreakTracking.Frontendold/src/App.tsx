import React, { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "./app/hooks";
import { initializeStreaks } from "./reducers/streakReducer";

function App() {
  const dispatch = useAppDispatch();
  const streaks = useAppSelector((state) => state.streaks);

  useEffect(() => {
    console.log("Fetching streaks...");
    dispatch(initializeStreaks());
  }, []);

  return (
    <div>
      <header className="bg-darkGrey  h-22">
        <h1>Streak Tracking</h1>
      </header>
    </div>
  );
}

export default App;
