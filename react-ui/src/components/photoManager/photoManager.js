import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Stack from "@mui/material/Stack";
import Container from "@mui/material/Container";
import ImageList from "@mui/material/ImageList";
import ImageListItem from "@mui/material/ImageListItem";
import ImageListItemBar from "@mui/material/ImageListItemBar";
import TextField from "@mui/material/TextField";
import { useEffect, useState } from "react";
import { uploadImage } from "~/services/photoManagerService";
import { ImageUpload } from "~/components/photoManager";

function PhotoManager() {
  const [fileSelected, setFileSelected] = useState(undefined);
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [url, setUrl] = useState("");
  const [images, setImages] = useState([]);

  useEffect(() => {
    return () => fileSelected && URL.revokeObjectURL(fileSelected.previewUrl);
  });

  useEffect(() => {
    return () => fileSelected && URL.revokeObjectURL(fileSelected.previewUrl);
  }, [fileSelected]);

  const handleChange = (e) => {
    const file = e.target.files[0];
    file.previewUrl = URL.createObjectURL(file);
    setFileSelected(file);
  };

  const handleSubmit = async () => {
    const request = {
      name: name,
      description: description,
      url: url,
    };

    console.log(request);
    try {
      const result = await uploadImage("ImageUpload/CreateImage", request);
      console.log("result", result);
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <Container maxWidth="lg">
      <Box sx={{ p: 2, mt: 2, border: "1px dashed grey" }}>
        <TextField
          fullWidth
          id="standard-basic"
          label="Image name"
          variant="standard"
          onChange={(e) => setName(e.target.value)}
        />
        <TextField
          fullWidth
          id="standard-basic"
          label="Description"
          variant="standard"
          onChange={(e) => setDescription(e.target.value)}
        />
        <TextField
          disabled
          fullWidth
          id="standard-basic"
          label="Url"
          variant="standard"
          value={url}
        />
        <Stack direction="row" alignItems="center" spacing={2} sx={{ mt: 2 }}>
          <ImageUpload callbackSetUrl={setUrl} />
        </Stack>
        <Button variant="contained" onClick={handleSubmit}>
          Update
        </Button>
      </Box>
      <Box sx={{ p: 2, mt: 2, border: "1px solid grey" }}>
        <ImageList sx={{ width: "100%", height: "100%" }} cols={3}>
          {images.map((item, index) => (
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
      </Box>
    </Container>
  );
}

export default PhotoManager;
