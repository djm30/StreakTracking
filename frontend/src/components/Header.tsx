import React from "react";
import GitHub from "./GitHub";

const Header = () => {
  return (
    <header className="bg-dark border-b-2 border-accent">
      <div className="container mx-auto">
        <div className="text-light h-20 flex justify-between items-center xl:mx-80 lg:mx-60 md:mx-32 sm:mx-20 mx-10">
          <h1 className="font-bold text-4xl">Streak Tracking</h1>
          <GitHub width="50" height="50" fill="white" />
        </div>
      </div>
    </header>
  );
};

export default Header;
