import * as React from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Stack from '@mui/material/Stack';
import { uploadImage } from "~/services/photoManagerService";
import { useState } from "react";
import Skeleton from '@mui/material/Skeleton';
import UploadImageDialog from './uploadImageDialog'

function ImageUpload({ callbackSetUrl }) {
  const [open, setOpen] = useState(false);
  const [value, setValue] = useState(undefined);
  const [url, setUrl] = useState('');

  const handleClickListItem = () => {
    setOpen(true);
  };

  const handleUpload = async (fileSelected) => {
    const formData = new FormData();
    formData.append("file", fileSelected);

    try {
      const result = await uploadImage("ImageUpload/UploadImage", formData);
      setUrl(result.data.uri)
      callbackSetUrl(result.data.uri)
      //setUrl(result.data)
    } catch (error) {
      console.error(error);
    }
  };

  const handleClose = async (newValue) => {

    setOpen(false);

    if (newValue) {
      await handleUpload(newValue)
    }
  };

  return (
    <Box sx={{ width: '100%', maxWidth: 360, bgcolor: 'background.paper' }}>
      <Stack direction="row"
        justifyContent="flex-start"
        alignItems="flex-end" spacing={2} sx={{ py: 2 }}>
        {/* {url && <img src={url} alt={url} css={css`
                  object-fit: contain;
                  width: 45%;
                  height: auto;
                `} />} */}
        {url ? (
          <img
            style={{ width: 210, height: 118 }}
            alt={url}
            src={url}
          />
        ) : (
          <Skeleton variant="rectangular" width={210} height={118} />
        )}


        <Button size="small" variant="outlined" onClick={handleClickListItem}>
          Select Photo
        </Button>
      </Stack>
      <UploadImageDialog
        id="ringtone-menu"
        keepMounted
        open={open}
        onClose={handleClose}
        value={value}
      />
    </Box>
  );
}
export default ImageUpload