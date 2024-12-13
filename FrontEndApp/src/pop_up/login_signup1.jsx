
import rectangle74 from "../assets/Rectangle 74.png";

export const LoginSignup1 = () => {
    return (
        <div className="relative w-[418px] h-[391px] bg-white rounded-[42px] shadow-[10px_10px_8.1px_-1px_#00000040]">
            <img
                className="absolute w-[13px] h-[13px] top-[34px] left-[359px] object-cover"
                alt="Rectangle"
                src={rectangle74}
            />

            <p className="absolute w-80 top-24 left-[51px] [font-family:'Noto_Sans-Bold',Helvetica] font-bold text-black text-base text-center tracking-[0] leading-[normal]">
                Влез и открий най-интересните събития около теб!
            </p>

            <div className="absolute w-[350px] h-[61px] top-[230px] left-9 rounded-[85px] border border-solid border-[#5abab7]">
                <div className="absolute w-[186px] h-[61px] top-0 left-[164px] bg-[#40ddc7] rounded-[85px] border border-solid border-[#5abab7]">
                    <div className="left-9 text-white absolute w-[111px] top-5 [font-family:'Noto_Sans-Medium',Helvetica] font-medium text-base text-center tracking-[0] leading-[normal] whitespace-nowrap">
                        Организатор
                    </div>
                </div>

                <div className="left-7 text-[#28666e] absolute w-[111px] top-5 [font-family:'Noto_Sans-Medium',Helvetica] font-medium text-base text-center tracking-[0] leading-[normal] whitespace-nowrap">
                    Потребител
                </div>
            </div>

            <div className="absolute w-[274px] top-44 left-[75px] [font-family:'Noto_Sans-Medium',Helvetica] font-medium text-black text-base text-center tracking-[0] leading-[normal]">
                Продължи като
            </div>
        </div>
    );
};
