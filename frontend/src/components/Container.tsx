import React from "react";

interface Props {
  children: Array<JSX.Element>;
}

const Container = ({ children }: Props) => {
  return (
    <section className="container mx-auto mt-32 ">
      <div className="xl:mx-40 lg:mx-32 md:mx-10 mx-10 grid xl:grid-cols-5 grid-cols-1  ">
        {children}
      </div>
    </section>
  );
};

export default Container;
