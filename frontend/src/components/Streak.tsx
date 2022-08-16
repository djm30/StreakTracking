import React, { useState, useRef } from "react";
import { FullStreak } from "../types";
import { differenceInDays } from "date-fns";
import { Options, useLongPress } from "../app/hooks";
import Complete from "./Complete";

interface Props {
  streak: FullStreak;
}

let timeout: ReturnType<typeof setTimeout>;

type dateId = string | undefined;

const getDate = (date: dateId): Date =>
  date ? new Date(date as string) : new Date("20/04/1972");

const isCompleteToday = (latestCompletion: Date): boolean =>
  differenceInDays(new Date(), latestCompletion) === 0 ? true : false;

const Streak: React.FC<Props> = ({ streak }) => {
  const divRef = useRef<HTMLDivElement>(null);

  const onLongPress = () => {
    divRef.current?.classList.remove("bg-green-100");
  };

  const onClick = () => {
    divRef.current?.classList.add("bg-green-100");
  };

  const defaultOptions: Options = {
    shouldPreventDefault: true,
    delay: 600,
  };

  const baseLongPressEvents = useLongPress(
    { onLongPress, onClick },
    defaultOptions,
  );

  const customLongPressEvents = {
    ...baseLongPressEvents,
    onMouseUp: (e: React.MouseEvent) => {
      baseLongPressEvents.onMouseUp(e);
      divRef.current?.classList.remove("bg-green-100");
    },
    onMouseDown: (e: React.MouseEvent) => {
      baseLongPressEvents.onMouseDown(e);
      divRef.current?.classList.add("bg-green-100");
    },
  };

  let latestCompletionDate = getDate(streak.completions.at(0)?.id);
  let isComplete = isCompleteToday(latestCompletionDate);

  return (
    <div
      ref={divRef}
      {...customLongPressEvents}
      className="h-[250px] w-[250px] bg-offGrey cursor-pointer rounded-[10px] border-2 border-accent flex flex-col items-center justify-between py-4 text-light font-bold hover:scale-105 transition-all select-none"
    >
      <h4 className="text-lg">{streak.streakName}</h4>
      <h5 className="text-5xl">{streak.currentStreak}</h5>
      <span className="block">
        <Complete complete={isComplete} />
      </span>
    </div>
  );
};

export default Streak;
