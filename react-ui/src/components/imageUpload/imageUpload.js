import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import IconButton from "@mui/material/IconButton";
import PhotoCamera from "@mui/icons-material/PhotoCamera";
import Stack from "@mui/material/Stack";
import Grid from "@mui/material/Grid";
import Container from "@mui/material/Container";
import ImageList from "@mui/material/ImageList";
import ImageListItem from "@mui/material/ImageListItem";
import ImageListItemBar from "@mui/material/ImageListItemBar";
import TextField from "@mui/material/TextField";
import { useEffect, useState } from "react";
import { css } from "@emotion/react";
import { uploadImage } from "~/services/uploadService";

function UploadImage() {
  const [fileSelected, setFileSelected] = useState(undefined);
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [url, setUrl] = useState("");
  const [files, setFiles] = useState([]);

  useEffect(() => {
    return () => fileSelected && URL.revokeObjectURL(fileSelected.previewUrl);
  }, [fileSelected]);

  const handleChange = (e) => {
    const file = e.target.files[0];
    file.previewUrl = URL.createObjectURL(file);
    setFileSelected(file);
  };

  const handleUpload = () => {
    const formData = new FormData();
    formData.append("file", fileSelected);

    submit(formData);
  };

  const submit = async (file) => {
    console.log(file);
    const payload = {
      name: name,
      description: description,
      file: file,
    };
    console.log('payload',payload);
    try {
      //const result = await uploadImage("ImageUpload/upload/image/", payload);
      //console.log("result", result);
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
          fullWidth
          id="standard-basic"
          label="Url"
          variant="standard"
          onChange={(e) => setName(e.target.value)}
        />
        <Stack direction="row" alignItems="center" spacing={2}>
          <div
            css={css`
              display: flex;
              flex-direction: column;
              align-items: start;
              margin-bottom: 1rem;
            `}
          >
            <IconButton
              color="primary"
              aria-label="upload picture"
              component="label"
            >
              <input
                id="fileUpload"
                hidden
                accept="image/*"
                multiple
                type="file"
                onChange={handleChange}
                css={css`
                  margin-bottom: 1rem;
                `}
              />
              <PhotoCamera />
              <label
                htmlFor="fileUpload"
                css={css`
                  font-size: 0.875rem;
                `}
              >
                Choose File
              </label>
            </IconButton>
            {fileSelected && (
              <img
                src={fileSelected.previewUrl}
                alt={fileSelected.name}
                css={css`
                  object-fit: contain;
                  width: 45%;
                  height: auto;
                `}
              />
            )}
          </div>
        </Stack>
        <Button variant="contained" onClick={handleUpload}>
          Submit
        </Button>
        {/* <div
          css={css`
            display: flex;
            flex-direction: column;
            margin-bottom: 1rem;
          `}
        >
          <input
            type="file"
            onChange={handleChange}
            css={css`
              margin-bottom: 1rem;
            `}
          ></input>
          {fileSelected && (
            <img
              src={fileSelected.previewUrl}
              alt={fileSelected.name}
              css={css`
                object-fit: contain;
                width: 45%;
                height: auto;
              `}
            />
          )}
        </div> */}
      </Box>
      <Box sx={{ p: 2, mt: 2, border: "1px solid grey" }}>
        <ImageList sx={{ width: "100%", height: "100%" }} cols={3}>
          {files.map((item, index) => (
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

export default UploadImage;
