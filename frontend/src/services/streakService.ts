import axios from "axios";
import { FullStreak } from "../types";

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

interface AddPostRequest {
    streakName: string,
    streakDescription: string
}
const postStreak = async (streakName: string, streakDescription: string) => {
    const body: AddPostRequest = { streakName, streakDescription }
    try {
        const { data } = await axios.post<{ message: string }>("/streaks", body);
        console.log(data)
    } catch (error) {
        console.error(error)
    }
}

export { getStreaks, postStreak }