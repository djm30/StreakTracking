import { createSlice, Dispatch, PayloadAction } from "@reduxjs/toolkit";
import { FullStreak } from "../types";
import { getStreaks } from "../services/streakService";

const initialState: FullStreak[] = [];

const streaksSlice = createSlice({
    name: "streaks",
    initialState,
    reducers: {
        setStreaks(state, action: PayloadAction<Array<FullStreak>>) {
            return action.payload;
        },
        replaceStreak(state, action: PayloadAction<FullStreak>) {

        },
        deleteStreak(state, action: PayloadAction<number>) {

        }
    }
})


const { setStreaks, replaceStreak, deleteStreak } = streaksSlice.actions;

export const initializeStreaks = () => {
    return async (dispatch: Dispatch<PayloadAction<Array<FullStreak>>>) => {
        const streaks = await getStreaks();
        dispatch(setStreaks(streaks));
    }
}

let reducer = streaksSlice.reducer;
export default reducer;