import React from "react";
import { useAppSelector } from "../app/hooks";
import { NotificationType } from "../types";

const Notification = () => {
  const notification = useAppSelector((state) => state.notification);

  const position = notification.message ? "left-5" : "-left-48";
  const border = "border-[#a5b3cc]";
  //   let position = "left-5";

  return (
    <div
      className={`fixed top-24 z-50 ${position} ${border} rounded-[12px] text-light border-2 transition-all`}
    >
      <div className="bg-accentInactive py-2 px-10  text-center rounded-t-[10px]">
        {notification.title}
      </div>
      <div className="bg-accent py-3 px-10 rounded-b-[10px]">
        {notification.message}
      </div>
    </div>
  );
};

export default Notification;
