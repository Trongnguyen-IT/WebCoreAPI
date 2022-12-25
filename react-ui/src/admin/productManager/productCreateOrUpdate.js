import * as React from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import FormControl from '@mui/material/FormControl';
import FormControlLabel from '@mui/material/FormControlLabel';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import Select from '@mui/material/Select';
import Switch from '@mui/material/Switch';

import Stack from "@mui/material/Stack";
import Container from "@mui/material/Container";
import ImageList from "@mui/material/ImageList";
import ImageListItem from "@mui/material/ImageListItem";
import ImageListItemBar from "@mui/material/ImageListItemBar";
import TextField from "@mui/material/TextField";
import { useEffect, useState } from "react";
import { getImages, uploadImage } from "~/services/photoManagerService";
import { getProducts, createProduct } from "~/services/productService";
import ImageUpload from "~/components/imageUpload";
import GetUrl from "~/utilities/getUrl";

export default function ProductCreateOrUpdate({open}) {
  const [fullWidth, setFullWidth] = React.useState(true);
  const [maxWidth, setMaxWidth] = React.useState('lg');
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [price, setPrice] = useState("");
  const [quantity, setQuantity] = useState("");
  const [url, setUrl] = useState("");
  const [images, setImages] = useState([]);
  const handleClose = () => {
    //setOpen(false);
  };
  const handleSubmit = async () => {
    const request = {
      name: name,
      price: 10000,
      promotion: 1000,
      quantity: 10,
      avatarUrl: url,
      imageUrls: [
        "https:localhost:7052/images/file_e7b60f50-3f0d-413a-b355-62f099ab96ee.jpg",
        "https:localhost:7052/images/file_e7b60f50-3f0d-413a-b355-62f099ab96ee.jpg",
      ],
    };

    try {
      const result = await createProduct("Product", request);
      //await loadData();
    } catch (error) {
      console.error(error);
    }
  };
  const handleMaxWidthChange = (event) => {
    setMaxWidth(
      // @ts-expect-error autofill of arbitrary value is not handled.
      event.target.value,
    );
  };

  const handleFullWidthChange = (event) => {
    setFullWidth(event.target.checked);
  };

  return (
    <React.Fragment>
     
      <Dialog
        fullWidth={fullWidth}
        maxWidth={maxWidth}
        open={open}
        onClose={handleClose}
      >
        <DialogTitle>Optional sizes</DialogTitle>
        <DialogContent>
          <DialogContentText>
            You can set my maximum width and whether to adapt or not.
          </DialogContentText>
          <Box
        sx={{
          "& .MuiTextField-root": { mb: 1 },
        }}
      >
        <TextField
          fullWidth
          label="Product name"
          variant="standard"
          onChange={(e) => setName(e.target.value)}
        />
        <TextField
          fullWidth
          label="Description"
          multiline
          rows={4}
          onChange={(e) => setDescription(e.target.value)}
        />
        <Stack direction="row" spacing={2}>
          <TextField
            fullWidth
            label="Price"
            variant="standard"
            value={price}
            onChange={(e) => setPrice(e.target.value)}
          />
          <TextField
            fullWidth
            label="Quantity"
            variant="standard"
            value={quantity}
            onChange={(e) => setQuantity(e.target.value)}
          />
        </Stack>

        <Stack direction="row" alignItems="center" spacing={2} sx={{ mt: 2 }}>
          <ImageUpload onSetUrl={setUrl} />
        </Stack>
        <Button variant="contained" onClick={handleSubmit}>
          Create
        </Button>

        {images && (
          <ImageList sx={{ width: "100%", height: "100%" }} cols={3}>
            {images.map((item, index) => (
              <ImageListItem key={index}>
                <img
                  src={`${GetUrl(
                    item.avatarUrl
                  )}?w=248&fit=crop&auto=format&dpr=2 2x`}
                  srcSet={`${item.url}?w=248&fit=crop&auto=format&dpr=2 2x`}
                  alt={item.title}
                  loading="lazy"
                />
                <ImageListItemBar
                  title={item.name}
                  subtitle={<span>by: {item.description}</span>}
                  position="below"
                />
              </ImageListItem>
            ))}
          </ImageList>
        )}
      </Box>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose}>Close</Button>
        </DialogActions>
      </Dialog>
    </React.Fragment>
  );
}