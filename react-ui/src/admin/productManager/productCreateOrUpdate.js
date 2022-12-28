import * as React from "react";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogTitle from "@mui/material/DialogTitle";
import IconButton from "@mui/material/IconButton";
import CloseIcon from '@mui/icons-material/Close';
import Stack from "@mui/material/Stack";
import TextField from "@mui/material/TextField";
import { useState } from "react";
import { createProduct } from "~/services/productService";
import ImageUpload from "~/components/imageUpload";

export default function ProductCreateOrUpdate({ open, onClose, onLoadData }) {
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [price, setPrice] = useState("");
  const [quantity, setQuantity] = useState("");
  const [url, setUrl] = useState("");
  const handleClose = () => {
    onClose(false);
    onLoadData();
  };
  const handleSubmit = async () => {
    const request = {
      name: name,
      description: description,
      price: price,
      promotion: 10,
      quantity: quantity,
      avatarUrl: url,
      imageUrls: [
        "https:localhost:7052/images/file_e7b60f50-3f0d-413a-b355-62f099ab96ee.jpg",
        "https:localhost:7052/images/file_e7b60f50-3f0d-413a-b355-62f099ab96ee.jpg",
      ],
    };

    try {
      const result = await createProduct("Product", request);
      if (result.status === 200) {
        handleClose();
      }
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <React.Fragment>
      <Dialog fullWidth maxWidth="md" open={open} onClose={handleClose}>
        <DialogTitle>
          Product manager
          {onClose ? (
            <IconButton
              aria-label="close"
              onClick={onClose}
              sx={{
                position: "absolute",
                right: 8,
                top: 8,
                color: (theme) => theme.palette.grey[500],
              }}
            >
              <CloseIcon />
            </IconButton>
          ) : null}
        </DialogTitle>
        <DialogContent>
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
            <Stack
              direction="row"
              alignItems="center"
              spacing={2}
              sx={{ mt: 2 }}
            >
              <ImageUpload onSetUrl={setUrl} />
            </Stack>
            <Button variant="contained" onClick={handleSubmit}>
              Create
            </Button>
          </Box>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose}>Close</Button>
        </DialogActions>
      </Dialog>
    </React.Fragment>
  );
}
