import { configureStore, ThunkAction, Action } from '@reduxjs/toolkit';
import streaksRecuder from "../reducers/streakReducer";
import notificationReducer from '../reducers/notificationReducer';

export const store = configureStore({
  reducer: {
    streaks: streaksRecuder,
    notification: notificationReducer
  },
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  Action<string>
>;
