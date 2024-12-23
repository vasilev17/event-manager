import React from "react";

export default function Popup({ message, onClose }) {
  return (
    <div className="fixed top-0 left-0 w-full h-full bg-black/50 flex justify-center items-center z-50">
      <div className="bg-white p-6 rounded-3xl shadow-lg min-w-[300px]">
        <div className="flex justify-end mb-4">
          <button
            className="text-xl cursor-pointer border-none bg-none"
            onClick={onClose}
          >
            &times;
          </button>
        </div>
        <div className="mb-4 mr-4">
          <p>{message}</p>
        </div>
        <div className="flex justify-end">
          <button
            className="px-4 py-2 rounded-full bg-primary text-white font-se cursor-pointer"
            onClick={onClose}
          >
            OK
          </button>
        </div>
      </div>
    </div>
  );
}
