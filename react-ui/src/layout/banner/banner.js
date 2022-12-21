// Import Swiper React components
import { Swiper, SwiperSlide } from "swiper/react";

// Import Swiper styles
import "swiper/css";
import banner1 from "~/assets/banners/banner1.jpg";
import banner2 from "~/assets/banners/banner2.jpg";
import banner3 from "~/assets/banners/banner3.jpg";
import { css } from "@emotion/react";
const banners = [banner1, banner2, banner3];
function Banner() {
  return (
    <Swiper
      spaceBetween={50}
      slidesPerView={1}
      onSlideChange={() => {}}
      onSwiper={(swiper) => {}}
    >
      {banners.map((item, index) => (
        <SwiperSlide key={index}>
          <img
            src={item}
            alt={item}
            css={css`
              object-fit: cover;
              max-height: 1080px;
              width: 100%;
            `}
          />
        </SwiperSlide>
      ))}
    </Swiper>
  );
}

export default Banner;
