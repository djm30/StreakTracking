import { createSlice, current, Dispatch, PayloadAction } from "@reduxjs/toolkit";
import { FullStreak, StreakComplete } from "../types";
import { getStreaks, getStreakById, markStreakComplete, postStreak, deleteStreakById } from "../services/streakService";
import { format } from "date-fns";

const initialState: FullStreak[] = [];

const addStreakComplete = (streak: FullStreak) => {
    streak.completions.unshift({ id: format(new Date(), 'yyyy-MM-dd'), complete: true, streakId: streak.streakId })
    streak.currentStreak++;
}

const removeStreakComplete = (streak: FullStreak) => {
    streak.completions.shift();
    streak.currentStreak--;
}

const streaksSlice = createSlice({
    name: "streaks",
    initialState,
    reducers: {
        setStreaks(state, action: PayloadAction<Array<FullStreak>>) {
            return action.payload;
        },
        appendStreak(state, action: PayloadAction<FullStreak>) {
            return state.concat(action.payload);
        },
        updateStreak(state, action: PayloadAction<FullStreak>) {
            return state.map(streak => streak.streakId === action.payload.streakId ? action.payload : streak);
        },
        completeStreak(state, action: PayloadAction<{ id: string, complete: boolean }>) {
            const updateStreak = action.payload.complete ? addStreakComplete : removeStreakComplete;
            state.forEach(streak => { if (streak.streakId === action.payload.id) { updateStreak(streak) } });
        },
        removeStreak(state, action: PayloadAction<string>) {
            return state.filter(streak => streak.streakId !== action.payload);
        }
    }
})


const { setStreaks, completeStreak, appendStreak, removeStreak } = streaksSlice.actions;

export const initializeStreaks = () => {
    return async (dispatch: Dispatch) => {
        const streaks = await getStreaks();
        dispatch(setStreaks(streaks));
    }
}

export const markComplete = (id: string, date: Date, complete = true) => {
    return async (dispatch: Dispatch) => {
        const returnMessage = await markStreakComplete(id, date, complete);
        dispatch(completeStreak({ id, complete }));
    }
}

export const addStreak = (streakName: string, streakDescription: string) => {
    return async (dispatch: Dispatch) => {
        const streakId = await postStreak(streakName, streakDescription) as string;
        const streak: FullStreak = { streakName, streakDescription, streakId, longestStreak: 0, currentStreak: 0, completions: [] }
        dispatch(appendStreak(streak))
    }
}

export const deleteStreak = (id: string) => {
    return async (dispatch: Dispatch) => {
        const response = await deleteStreakById(id);
        console.log(response);
        dispatch(removeStreak(id));
    }
}

let reducer = streaksSlice.reducer;
export default reducer;