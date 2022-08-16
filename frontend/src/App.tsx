import React, { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "./app/hooks";
import Container from "./components/Container";
import Header from "./components/Header";
import StreakForm from "./components/StreakForm";
import Streaks from "./components/Streaks";
import Notification from "./components/Notification";

import { initializeStreaks } from "./reducers/streakReducer";

function App() {
  const dispatch = useAppDispatch();

  useEffect(() => {
    console.log("Fetching streaks...");
    dispatch(initializeStreaks());
  }, []);

  return (
    <div>
      <Header />
      <Notification />
      <Container>
        {/* FORM */}
        <StreakForm />
        {/* STREAKS */}
        <Streaks />
      </Container>
    </div>
  );
}

export default App;
