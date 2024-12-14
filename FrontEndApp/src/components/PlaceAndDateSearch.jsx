export default function () {
  return (
    <div className="bg-cover mb-20 bg-center rounded-b-3xl bg-no-repeat w-full h-[800px] relative bg-[url('D:\S_tasks\ST\EventManager\FrontEndApp\src\assets\concertBackgroung.jpg')]">
      <div className="w-[1000px] h-36 bg-white rounded-full shadow-xl absolute bottom-0 left-0 m-14 p-6 flex">
        <div className="input-flex">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            stroke-width="1.5"
            stroke="currentColor"
            class="size-6"
          >
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              d="m21 21-5.197-5.197m0 0A7.5 7.5 0 1 0 5.196 5.196a7.5 7.5 0 0 0 10.607 10.607Z"
            />
          </svg>

          <input type="text" placeholder="Въведете място или изпълнител " className="focus:outline-none p-2 border-b-2 ml-1 w-64"></input>
        </div>
        <div className="input-flex">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            stroke-width="1.5"
            stroke="currentColor"
            class="size-6"
          >
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              d="M6.75 3v2.25M17.25 3v2.25M3 18.75V7.5a2.25 2.25 0 0 1 2.25-2.25h13.5A2.25 2.25 0 0 1 21 7.5v11.25m-18 0A2.25 2.25 0 0 0 5.25 21h13.5A2.25 2.25 0 0 0 21 18.75m-18 0v-7.5A2.25 2.25 0 0 1 5.25 9h13.5A2.25 2.25 0 0 1 21 11.25v7.5m-9-6h.008v.008H12v-.008ZM12 15h.008v.008H12V15Zm0 2.25h.008v.008H12v-.008ZM9.75 15h.008v.008H9.75V15Zm0 2.25h.008v.008H9.75v-.008ZM7.5 15h.008v.008H7.5V15Zm0 2.25h.008v.008H7.5v-.008Zm6.75-4.5h.008v.008h-.008v-.008Zm0 2.25h.008v.008h-.008V15Zm0 2.25h.008v.008h-.008v-.008Zm2.25-4.5h.008v.008H16.5v-.008Zm0 2.25h.008v.008H16.5V15Z"
            />
          </svg>
          <input type="text" placeholder="Въведете дата" className="focus:outline-none p-2 border-b-2 ml-1 w-64"></input>
        </div>
        <button className="mt-1 w-[219px] h-[89px] justify-end bg-primary rounded-full text-center text-white text-3xl font-semibold font-sans" >Търси</button>
      </div>
    </div>
  );
}
