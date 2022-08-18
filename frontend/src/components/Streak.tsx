import React, { useState, useRef } from "react";
import { FullStreak, NotificationType } from "../types";
import { differenceInDays } from "date-fns";
import { Options, useLongPress } from "../app/hooks";
import Complete from "./Complete";
import { useAppDispatch } from "../app/hooks";
import { setNotification } from "../reducers/notificationReducer";
import { markComplete, deleteStreak } from "../reducers/streakReducer";

import styles from "./Streak.module.css";
import TrashBin from "./TrashBin";

interface Props {
  streak: FullStreak;
}

type dateId = string | undefined;

const getDate = (date: dateId): Date =>
  date ? new Date(date as string) : new Date("20/04/1972");

const isCompleteToday = (latestCompletion: Date): boolean =>
  differenceInDays(new Date(), latestCompletion) === 0 ? true : false;

const Streak: React.FC<Props> = ({ streak }) => {
  let latestCompletionDate = getDate(streak.completions.at(0)?.id);
  let isComplete = isCompleteToday(latestCompletionDate);

  const dispatch = useAppDispatch();

  const divRef = useRef<HTMLDivElement>(null);

  const onLongPress = () => {
    if (isComplete) {
      dispatch(markComplete(streak.streakId, new Date(), false));
      dispatch(
        setNotification("Streak Marked Incomplete!", NotificationType.SUCCESS),
      );
    } else {
      dispatch(markComplete(streak.streakId, new Date(), true));
      dispatch(
        setNotification("Streak Marked Complete!", NotificationType.SUCCESS),
      );
    }
  };

  const remove = () => {
    dispatch(setNotification("Streak Deleted!", NotificationType.SUCCESS));
    dispatch(deleteStreak(streak.streakId));
  };

  const defaultOptions: Options = {
    shouldPreventDefault: true,
    delay: 500,
  };

  const baseLongPressEvents = useLongPress(
    { onLongPress, onClick: () => {} },
    defaultOptions,
  );

  return (
    <div
      ref={divRef}
      {...baseLongPressEvents}
      className={`xl:h-[250px] xl:w-[250px]  bg-offGrey  cursor-pointer rounded-[10px] border-2 border-accent flex flex-col items-center justify-between py-4 text-light font-bold hover:scale-105 transition-all select-none ${styles.streak}`}
    >
      <TrashBin
        className="absolute  right-2 bottom-2 fill-neutral-400 hover:fill-white z-10"
        onClick={remove}
      />
      <h4 className="xl:text-lg md:text-base">{streak.streakName}</h4>
      <h5 className="xl:text-5xl md:text-3xl">{streak.currentStreak}</h5>
      <span className="block">
        <Complete complete={isComplete} />
      </span>
    </div>
  );
};

export default Streak;
