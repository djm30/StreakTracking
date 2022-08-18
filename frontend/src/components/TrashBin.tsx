import React from "react";

interface Props {
  className?: string;
  onClick: () => void;
}

const TrashBin = ({ className, onClick }: Props) => {
  return (
    <svg
      onClick={(e) => {
        e.stopPropagation();
        onClick();
      }}
      className={className}
      width="24"
      height="24"
      xmlns="http://www.w3.org/2000/svg"
      fillRule="evenodd"
      clipRule="evenodd"
    >
      <path d="M19 24h-14c-1.104 0-2-.896-2-2v-17h-1v-2h6v-1.5c0-.827.673-1.5 1.5-1.5h5c.825 0 1.5.671 1.5 1.5v1.5h6v2h-1v17c0 1.104-.896 2-2 2zm-14-2.5c0 .276.224.5.5.5h13c.276 0 .5-.224.5-.5v-16.5h-14v16.5zm5-18.5h4v-1h-4v1z" />
    </svg>
  );
};

export default TrashBin;
