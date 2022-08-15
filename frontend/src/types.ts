export interface Streak {
    streakId: string,
    streakName: string,
    streakDescription: string,
    longestStreak: number
};

export interface StreakComplete {
    id: Date,
    streakId: string,
    complte: boolean
}

export interface CurrentStreak {
    streak: number,
    currentDate: Date
}

export interface FullStreak {
    streakId: string,
    streakName: string,
    streakDescription: string,
    longestStreak: number
    currentStreak: number
    completions: StreakComplete[]
}