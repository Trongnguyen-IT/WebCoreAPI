import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Stack from "@mui/material/Stack";
import Container from "@mui/material/Container";
import ImageList from "@mui/material/ImageList";
import ImageListItem from "@mui/material/ImageListItem";
import ImageListItemBar from "@mui/material/ImageListItemBar";
import TextField from "@mui/material/TextField";
import { useEffect, useState } from "react";
import { getImages, uploadImage } from "~/services/photoManagerService";
import { ImageUpload } from "~/components/photoManager";
import { useLoaderData } from "react-router-dom";

function PhotoManager() {
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [url, setUrl] = useState("");
  const [images, setImages] = useState([]);

  useEffect(() => {
    loadData()
  },[])

  const loadData = async () => {
    const param = {
      name: 'abc'
    }
    const result = await getImages('ImageUpload/Get', param)
    console.log('result', result);
    result && result.data && setImages(result.data)
  }
  const handleSubmit = async () => {
    const request = {
      name: name,
      description: description,
      url: url,
    };

    try {
      const result = await uploadImage("ImageUpload/CreateImage", request);
      await loadData()

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
          <ImageUpload onSetUrl={setUrl} />
        </Stack>
        <Button variant="contained" onClick={handleSubmit}>
          Update
        </Button>
      </Box>
      {images &&
        <ImageList sx={{ width: "100%", height: "100%" }} cols={3}>
          {images.map((item, index) => (
            <ImageListItem key={index}>
              <img
                src={`${item.url}?w=248&fit=crop&auto=format`}
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
      }
    </Container>
  );
}

export default PhotoManager;
