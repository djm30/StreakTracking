import React from "react";

const Tick: React.FC = ({ fill, height, width }: SvgProps) => (
  <svg
    xmlns="http://www.w3.org/2000/svg"
    width="42"
    height="42"
    fill="none"
    viewBox="0 0 42 42"
  >
    <path
      stroke="#2FC73E"
      strokeLinecap="round"
      strokeLinejoin="round"
      strokeWidth="1.5"
      d="m7.219 22.969 9.187 9.187L34.781 12.47"
    />
  </svg>
);
const Cross: React.FC = ({ fill, height, width }: SvgProps) => (
  <svg
    xmlns="http://www.w3.org/2000/svg"
    width="42"
    height="42"
    fill="none"
    viewBox="0 0 42 42"
  >
    <path
      stroke="#E60000"
      strokeLinecap="round"
      strokeLinejoin="round"
      strokeWidth="1.5"
      d="M29.531 12.469 12.47 29.53m0-17.062L29.53 29.53"
    />
  </svg>
);

interface SvgProps {
  fill?: string;
  width?: string;
  height?: string;
}

interface CompleteProps {
  complete: boolean;
  fill?: string;
  width?: string;
  height?: string;
}

const Complete = ({ complete, fill, width, height }: CompleteProps) => {
  const heightToUse = height ? height : "24";
  const widthToUse = width ? width : "24";

  const SVG: React.FC<SvgProps> = complete ? Tick : Cross;
  return <SVG width={widthToUse} height={heightToUse} fill={fill} />;
};

export default Complete;
