import * as React from "react";
import ImageList from "@mui/material/ImageList";
import ImageListItem from "@mui/material/ImageListItem";
import ImageListItemBar from "@mui/material/ImageListItemBar";
import Grid from "@mui/material/Grid";
import img1 from "~/assets/products/img1.jpg";
import img2 from "~/assets/products/img2.jpg";
import img3 from "~/assets/products/img3.jpg";
import img4 from "~/assets/products/img4.jpg";
import img5 from "~/assets/products/img5.jpg";
import img6 from "~/assets/products/img6.jpg";
import Paper from "@mui/material/Paper";
import { styled } from "@mui/material/styles";
const Item = styled(Paper)(({ theme }) => ({
  backgroundColor: theme.palette.mode === "dark" ? "#1A2027" : "#fff",
  ...theme.typography.body2,
  padding: theme.spacing(1),
  textAlign: "center",
  color: theme.palette.text.secondary,
}));
export default function TitlebarBelowImageList() {
  return (
    <ImageList sx={{ width: "100%", height: "100%" }} cols={3}>
      {itemData.map((item, index) => (
        <ImageListItem key={index}>
          <img
            src={`${item.img}?w=248&fit=crop&auto=format`}
            srcSet={`${item.img}?w=248&fit=crop&auto=format&dpr=2 2x`}
            alt={item.title}
            loading="lazy"
          />
          <ImageListItemBar
            title={item.title}
            subtitle={<span>by: {item.author}</span>}
            position="below"
          />
        </ImageListItem>
      ))}
    </ImageList>
  );
}

const itemData = [
  {
    img: img1,
    title: "Breakfast",
    author: "@bkristastucchio",
  },
  {
    img: img2,
    title: "Burger",
    author: "@rollelflex_graphy726",
  },
  {
    img: img3,
    title: "Camera",
    author: "@helloimnik",
  },
  {
    img: img4,
    title: "Coffee",
    author: "@nolanissac",
  },
  {
    img: img5,
    title: "Hats",
    author: "@hjrc33",
  },
  {
    img: img6,
    title: "Honey",
    author: "@arwinneil",
  },
  {
    img: img1,
    title: "Basketball",
    author: "@tjdragotta",
  },
  {
    img: img2,
    title: "Fern",
    author: "@katie_wasserman",
  },
  {
    img: img3,
    title: "Fern",
    author: "@katie_wasserman",
  },
  {
    img: img4,
    title: "Basketball",
    author: "@tjdragotta",
  },
  {
    img: img5,
    title: "Fern",
    author: "@katie_wasserman",
  },
  {
    img: img6,
    title: "Fern",
    author: "@katie_wasserman",
  },
];
