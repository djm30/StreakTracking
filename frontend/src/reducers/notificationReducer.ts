import { Notification, NotificationType } from "../types";
import { Action, createSlice, Dispatch, PayloadAction } from "@reduxjs/toolkit";

let timeout: ReturnType<typeof setTimeout>;
let initialState: Notification = { message: "", title: "", type: NotificationType.INFO }

const notificationSlice = createSlice({
    name: "notification",
    initialState,
    reducers: {
        setNotificationState(state, action: PayloadAction<Notification>) {
            return action.payload;
        },
    }
})

const { setNotificationState } = notificationSlice.actions;

export const setNotification = (message: string, type: NotificationType, title?: string,) => {
    const titleToUse: string = title ? title : type;
    return (dispatch: Dispatch<PayloadAction<Notification>>) => {
        clearTimeout(timeout);
        dispatch(setNotificationState({ title: titleToUse, message, type }))
        timeout = setTimeout(() => {
            dispatch(setNotificationState({ title: "", message: "", type: NotificationType.INFO }))
        }, 4000)
    }
}

export default notificationSlice.reducer;