export interface Streak {
    streakId: string,
    streakName: string,
    streakDescription: string,
    longestStreak: number
};

export interface StreakComplete {
    id: string,
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

export interface Notification {
    title: string,
    type: NotificationType,
    message: string
}

export enum NotificationType {
    INFO = "Info",
    ERROR = "Error",
    SUCCESS = "Success"
}