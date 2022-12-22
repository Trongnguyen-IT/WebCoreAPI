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

function UploadImageDialog(props) {
    const { url: urlProp, setUrl, onClose, value: valueProp, open, ...other } = props;
    const [value, setValue] = useState(valueProp);
    const [fileSelected, setFileSelected] = useState(undefined);
  
    useEffect(() => {
      return () => fileSelected && URL.revokeObjectURL(fileSelected.previewUrl);
    }, [fileSelected]);
  
    const handleChange = (e) => {
      const file = e.target.files[0];
      file.previewUrl = URL.createObjectURL(file);
      setFileSelected(file);
      setValue(file)
    };
  
    useEffect(() => {
      if (!open) {
        setValue(valueProp);
      }
    }, [valueProp, open]);
  
    const handleEntering = () => {
      console.log('enter');
    };
  
    const handleCancel = () => {
      onClose();
    };
  
    const handleOk = () => {
      onClose(value);
    };
  
    return (
      <Dialog
        sx={{ '& .MuiDialog-paper': { width: '80%', maxHeight: 435 } }}
        maxWidth="xs"
        TransitionProps={{ onEntering: handleEntering }}
        open={open}
        {...other}
      >
        <DialogTitle>Upload Photo</DialogTitle>
        <DialogContent dividers>
          <Box sx={{ p: 2, mt: 2, border: "1px dashed grey" }}>
            <Stack>
              {!fileSelected && <IconButton
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
                <CloudUpload sx={{ mr: 1 }} />
  
                <label
                  htmlFor="fileUpload"
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
          <Button autoFocus onClick={handleCancel}>
            Cancel
          </Button>
          <Button variant="contained" onClick={handleOk}>Save</Button>
        </DialogActions>
      </Dialog>
    );
  }

  export default UploadImageDialog