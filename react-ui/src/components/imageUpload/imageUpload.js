import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import IconButton from "@mui/material/IconButton";
import PhotoCamera from "@mui/icons-material/PhotoCamera";
import Stack from "@mui/material/Stack";
import { useEffect, useState } from "react";
import { css } from "@emotion/react";
import { uploadImage } from '~/services/uploadService'

function UploadImage() {
  const [fileSelected, setFileSelected] = useState(undefined);

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
    try {
      const result = await uploadImage('ImageUpload/upload/image/', file)
      console.log('result', result);
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <Box sx={{ p: 2, border: "1px dashed grey" }}>
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
  );
}

export default UploadImage;
