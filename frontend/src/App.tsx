import React, { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "./app/hooks";
import Complete from "./components/Complete";
import GitHub from "./components/GitHub";

import { initializeStreaks } from "./reducers/streakReducer";

function App() {
  const dispatch = useAppDispatch();
  const streaks = useAppSelector((state) => state.streaks);

  // useEffect(() => {
  //   console.log("Fetching streaks...");
  //   dispatch(initializeStreaks());
  // }, []);

  return (
    <div>
      <header className="bg-dark">
        <div className="container mx-auto">
          <div className="text-light h-20 flex justify-between items-center xl:mx-80 lg:mx-60 md:mx-32 sm:mx-20 mx-10">
            <h1 className="font-bold text-4xl">Streak Tracking</h1>
            <GitHub width="50" height="50" fill="white" />
          </div>
        </div>
      </header>
      <section className="container mx-auto mt-32 ">
        {/* FORM */}
        <div className="xl:mx-40 lg:mx-32 md:mx-10 mx-0 grid xl:grid-cols-5 lg:grid-cols-1  ">
          <div className="col-span-2 text-light  ">
            <form className="space-y-4">
              <div>
                <input
                  className="focus:outline-none focus:border-highlight bg-darkGrey rounded-[10px] w-80 px-4 py-2 border-2 border-accent"
                  placeholder="Streak Name"
                />
              </div>
              <div>
                <textarea
                  className="focus:outline-none focus:border-highlight bg-darkGrey rounded-[10px] w-80 px-4 py-4 border-2 border-accent"
                  placeholder="Streak Name"
                />
              </div>
              <button className="bg-accent hover:bg-highlight transition-colors rounded-[10px] px-8 py-2 ">
                Submit
              </button>
            </form>
          </div>
          {/* STREAKS */}
          <div className="col-span-3 grid lg:grid-cols-2 sm:grid-cols-1 md:ml-20 md:gap-10 gap-4">
            {/* SINGLE CARD */}
            <div className="h-[250px] w-[250px] bg-offGrey rounded-[10px] border-2 border-accent flex flex-col items-center justify-between py-4 text-light font-bold hover:scale-105 transition-all">
              <h4 className="text-lg">Gym</h4>
              <h5 className="text-5xl">12</h5>
              <span className="block">
                <Complete complete={false} />
              </span>
            </div>
            {/* SINGLE CARD */}
            <div className="h-[250px] w-[250px] bg-offGrey rounded-[10px] border-2 border-accent flex flex-col items-center justify-between py-4 text-light font-bold hover:scale-105 transition-all">
              <h4 className="text-lg">Gym</h4>
              <h5 className="text-5xl">12</h5>
              <span className="block">
                <Complete complete={false} />
              </span>
            </div>
            {/* SINGLE CARD */}
            <div className="h-[250px] w-[250px] bg-offGrey rounded-[10px] border-2 border-accent flex flex-col items-center justify-between py-4 text-light font-bold hover:scale-105 transition-all">
              <h4 className="text-lg">Gym</h4>
              <h5 className="text-5xl">12</h5>
              <span className="block">
                <Complete complete={false} />
              </span>
            </div>
            {/* SINGLE CARD */}
            <div className="h-[250px] w-[250px] bg-offGrey rounded-[10px] border-2 border-accent flex flex-col items-center justify-between py-4 text-light font-bold hover:scale-105 transition-all">
              <h4 className="text-lg">Gym</h4>
              <h5 className="text-5xl">12</h5>
              <span className="block">
                <Complete complete={false} />
              </span>
            </div>
          </div>
        </div>
      </section>
    </div>
  );
}

export default App;
