import axios from "axios";
import { FullStreak } from "../types";
import { format } from "date-fns";

const baseUrl = "http://localhost:3001/api/v1/streaks"
type GetStreaksResponse = {
    content: Array<FullStreak>
    message: string
}

const getStreaks = async () => {
    try {
        const { data } = await axios.get<GetStreaksResponse>(`/streaks/full`, {
            headers: {
                Accept: 'application/json',

            },
        },);
        return data.content;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log('error message: ', error.message);
        } else {
            console.log('unexpected error: ', error);
            throw new Error("Unexpected error occured")
        }
    }
    return new Array<FullStreak>
}

type GetStreakByIdResponse = {
    content: FullStreak
    message: string
}
const getStreakById = async (id: string) => {
    const { data } = await axios.get<GetStreakByIdResponse>(`/streaks/full/${id}`)
    return data.content;
}

interface AddPostRequest {
    streakName: string,
    streakDescription: string
}
const postStreak = async (streakName: string, streakDescription: string) => {
    const body: AddPostRequest = { streakName, streakDescription }
    try {
        const { data } = await axios.post<{ message: string }>("/streaks", body);
        return data.message.split(": ").at(1);
    } catch (error) {
        console.error(error)
    }
}

const markStreakComplete = async (streakId: string, date: Date, complete: boolean) => {

    const body = { complete, date: format(date, 'yyyy-MM-dd') }

    const { data } = await axios.post<{ message: string }>(`/streaks/complete/${streakId}`, body)
    return data.message;
}

const deleteStreakById = async (streakId: string) => {

    const { data } = await axios.delete<{ message: string }>(`/streaks/${streakId}`)
    return data.message;
}

export { getStreaks, postStreak, markStreakComplete, getStreakById, deleteStreakById }