import { createSlice, Dispatch, PayloadAction } from "@reduxjs/toolkit";
import { FullStreak } from "../types";
import { getStreaks, getStreakById, markComplete } from "../services/streakService";

const initialState: FullStreak[] = [];

const streaksSlice = createSlice({
    name: "streaks",
    initialState,
    reducers: {
        setStreaks(state, action: PayloadAction<Array<FullStreak>>) {
            return action.payload;
        },
        replaceStreak(state, action: PayloadAction<FullStreak>) {
            return state.map(streak => streak.streakId === action.payload.streakId ? action.payload : streak);
        },
        deleteStreak(state, action: PayloadAction<number>) {

        }
    }
})


const { setStreaks, replaceStreak, deleteStreak } = streaksSlice.actions;

export const initializeStreaks = () => {
    return async (dispatch: Dispatch) => {
        const streaks = await getStreaks();
        dispatch(setStreaks(streaks));
    }
}

export const completeStreak = (id: string, date: Date, complete = true) => {
    return async (dispatch: Dispatch) => {
        const returnMessage = await markComplete(id, date, complete);
        console.log(returnMessage)
        const updatedStreak = await getStreakById(id);
        dispatch(replaceStreak(updatedStreak));
    }
}

let reducer = streaksSlice.reducer;
export default reducer;