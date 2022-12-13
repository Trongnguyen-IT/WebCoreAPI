// Import Swiper React components
import { Swiper, SwiperSlide } from "swiper/react";

// Import Swiper styles
import "swiper/css";

function Banner() {
  return (
    <Swiper
      spaceBetween={50}
      slidesPerView={3}
      onSlideChange={() => console.log("slide change")}
      onSwiper={(swiper) => console.log(swiper)}
    >
      <SwiperSlide>
        <img src="../public/images/banners/1.jpg" />
      </SwiperSlide>
      <SwiperSlide>
        <img src="../public/images/banners/2.jpg" />
      </SwiperSlide>
      <SwiperSlide>
        <img src="../public/images/banners/3.jpg" />
      </SwiperSlide>
      <SwiperSlide>
        <img src="../public/images/banners/4.jpg" />
      </SwiperSlide>
      <SwiperSlide>
        <img src="../public/images/banners/5.jpg" />
      </SwiperSlide>
      <SwiperSlide>
        <img src="../public/images/banners/6.jpg" />
      </SwiperSlide>
    </Swiper>
  );
}

export default Banner;
