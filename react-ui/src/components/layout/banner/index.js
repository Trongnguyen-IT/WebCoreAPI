// Import Swiper React components
import { Swiper, SwiperSlide } from "swiper/react";

// Import Swiper styles
import "swiper/css";
import banner1 from "~/assets/banners/banner1.jpg";
import banner2 from "~/assets/banners/banner2.jpg";
import banner3 from "~/assets/banners/banner3.jpg";
import banner4 from "~/assets/banners/banner4.jpg";

function Banner() {
  return (
    <Swiper
      spaceBetween={50}
      slidesPerView={3}
      onSlideChange={() => console.log("slide change")}
      onSwiper={(swiper) => console.log(swiper)}
    >
      <SwiperSlide>
        <img src={banner1} />
      </SwiperSlide>
      <SwiperSlide>
        <img src={banner2} />
      </SwiperSlide>
      <SwiperSlide>
        <img src={banner3} />
      </SwiperSlide>
      <SwiperSlide>
        <img src={banner4} />
      </SwiperSlide>
    </Swiper>
  );
}

export default Banner;
