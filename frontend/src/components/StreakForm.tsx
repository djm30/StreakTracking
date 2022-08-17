import React, { FormEvent, useState } from "react";
import { postStreak } from "../services/streakService";
import { setNotification } from "../reducers/notificationReducer";
import { useAppDispatch } from "../app/hooks";
import { NotificationType } from "../types";

const StreakForm = () => {
  const [name, setName] = useState("");
  const [desc, setDesc] = useState("");
  const [buttonActive, setButtonActive] = useState(true);

  const dispatch = useAppDispatch();

  let timeout: ReturnType<typeof setTimeout>;

  const buttonClasses = buttonActive
    ? "bg-accent hover:bg-highlight"
    : "bg-accentInactive hover:bg-[#434c5c]";

  const onSubmit = async (e: FormEvent) => {
    e.preventDefault();
    if (!buttonActive) {
      dispatch(
        setNotification(
          "Please wait before submitting again...",
          NotificationType.INFO,
        ),
      );
      return;
    }
    if (!name || !desc) {
      dispatch(
        setNotification(
          "Please enter a name and descripiton",
          NotificationType.ERROR,
        ),
      );
      return;
    }
    const res = await postStreak(name, desc);
    console.log(res);
    setButtonActive(false);
    timeout = setTimeout(() => {
      setButtonActive(true);
    }, 5000);
    setName("");
    setDesc("");
  };

  return (
    <div className="col-span-2 text-light mx-auto mb-10  ">
      <form className="space-y-4" onSubmit={onSubmit}>
        <div>
          <input
            className="focus:outline-none focus:border-highlight bg-darkGrey rounded-[10px] w-80 px-4 py-2 border-2 border-accent"
            placeholder="Streak Name"
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
        </div>
        <div>
          <textarea
            className="focus:outline-none focus:border-highlight bg-darkGrey rounded-[10px] w-80 px-4 py-4 border-2 border-accent"
            placeholder="Streak Name"
            value={desc}
            onChange={(e) => setDesc(e.target.value)}
          />
        </div>
        <div className="flex justify-center xl:justify-start">
          <button
            className={`${buttonClasses} transition-colors rounded-[10px] px-8 py-2 `}
          >
            Submit
          </button>
        </div>
      </form>
    </div>
  );
};

export default StreakForm;
