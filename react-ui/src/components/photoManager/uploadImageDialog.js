import * as React from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Stack from '@mui/material/Stack';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import Dialog from '@mui/material/Dialog';
import CloudUpload from '@mui/icons-material/CloudUpload';
import { useEffect, useState } from "react";
import { css } from "@emotion/react";
import IconButton from "@mui/material/IconButton";
import { uploadImage } from "~/services/photoManagerService";
import CloseIcon from '@mui/icons-material/Close';
import Alert from '@mui/material/Alert';
import AlertTitle from '@mui/material/AlertTitle';

function UploadImageDialog(props) {
  const { url: urlProp, setUrl, onClose, fileSelected: fileSelectedProp, onSave, open, ...other } = props;
  const [fileSelected, setFileSelected] = useState(fileSelectedProp);

  useEffect(() => {
    setFileSelected(fileSelectedProp)
    return () => {
      fileSelected && URL.revokeObjectURL(fileSelected.previewUrl);
    }
  }, [fileSelectedProp, open]);

  const handleChange = (e) => {
    const file = e.target.files[0];
    file.previewUrl = URL.createObjectURL(file);
    setFileSelected(file);
  };

  const handleSave = () => {
    onSave(fileSelected)
  }

  const handleEntering = () => {
    console.log('enter');
  };

  const handleChangeImage = () => {
    //onClose();
  };

  return (
    <Dialog
      sx={{ '& .MuiDialog-paper': { width: '80%', maxHeight: 435 } }}
      maxWidth="xs"
      TransitionProps={{ onEntering: handleEntering }}
      open={open}
      {...other}
    >
      <DialogTitle>Upload Photo
        {onClose ? (
          <IconButton
            aria-label="close"
            onClick={onClose}
            sx={{
              position: 'absolute',
              right: 8,
              top: 8,
              color: (theme) => theme.palette.grey[500],
            }}
          >
            <CloseIcon />
          </IconButton>
        ) : null}
      </DialogTitle>
      <DialogContent dividers>
        <Box sx={{ p: 2, mt: 2, border: "1px dashed grey" }}>
          <Stack>
            {!fileSelected && <IconButton
              color="primary"
              aria-label="upload picture"
              component="label"
            >
              <input
                id='input-upload'
                hidden
                accept="image/*"
                multiple
                type="file"
                onChange={handleChange}
                css={css`
                    margin-bottom: 1rem;
                  `}
              />
              <CloudUpload sx={{ mr: 1 }} />

              <label
                htmlFor="input-upload"
                css={css`
                    font-size: 0.875rem;
                  `}
              >
                Select photo
              </label>
            </IconButton>
            }
            {fileSelected && (
              <img
                src={fileSelected.previewUrl}
                alt={fileSelected.name}
                css={css`
                    object-fit: contain;
                  `}
              />
            )}
          </Stack>
        </Box>
      </DialogContent>
      <DialogActions>
        <Button disabled={!fileSelected} autoFocus onClick={handleChangeImage}>
          <IconButton
            color="primary"
            aria-label="upload picture"
            component="label"
          >
            <input
              id='input-change'
              hidden
              accept="image/*"
              multiple
              type="file"
              onChange={handleChange}
              css={css`
                    margin-bottom: 1rem;
                  `}
            />
            <CloudUpload sx={{ mr: 1 }} />

            <label
              htmlFor="input-change"
              css={css`
                    font-size: 0.875rem;
                  `}
            >
              Change
            </label>
          </IconButton>
        </Button>
        <Button variant="contained" onClick={handleSave}>Save</Button>
      </DialogActions>
    </Dialog>
  );
}

export default UploadImageDialog