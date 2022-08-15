import axios from "axios";
import { FullStreak } from "../types";

const baseUrl = "https://localhost:7101/api/v1/streaks"
type GetStreaksResponse = {
    content: Array<FullStreak>
    message: string
}

const getStreaks = async () => {
    try {
        const { data } = await axios.get<GetStreaksResponse>(`${baseUrl}/full`, {
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

export { getStreaks }