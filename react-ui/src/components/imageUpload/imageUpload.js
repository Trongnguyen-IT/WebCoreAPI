import * as React from "react";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Stack from "@mui/material/Stack";
import { uploadImage } from "~/services/photoManagerService";
import { useState, useEffect } from "react";
import Skeleton from "@mui/material/Skeleton";
import ImageUploadDialog from "./imageUploadDialog";
import Alert from "@mui/material/Alert";
import IconButton from "@mui/material/IconButton";
import CloudUpload from "@mui/icons-material/CloudUpload";
import GetUrl from "~/utilities/getUrl";

export default function ImageUpload({ onSetUrl }) {
  const [open, setOpen] = useState(false);
  const [isAlert, setIsAlert] = useState(false);
  const [fileSelected, setFileSelected] = useState(undefined);
  const [url, setUrl] = useState(undefined);

  useEffect(() => {
    console.log("fileSelected", fileSelected);
    return () => {
      fileSelected && URL.revokeObjectURL(fileSelected.previewUrl);
    };
  }, [fileSelected]);

  const showModalUpload = () => {
    setOpen(true);
  };

  const handleUpload = async (fileSelected) => {
    const formData = new FormData();
    formData.append("file", fileSelected);

    try {
      const result = await uploadImage("ImageUpload/UploadImage", formData);
      console.log("resulthandleUpload", result);
      if (result.status === 200) {
        setUrl(result.data);
        onSetUrl(result.data);
        handleClose();
      }
    } catch (error) {
      console.error("error", error.message);
    }
  };

  const handleClose = async (newValue) => {
    setOpen(false);
  };

  return (
    <Box sx={{ width: "100%", maxWidth: 360, bgcolor: "background.paper" }}>
      <Box
        sx={{
          width: "50%",
          height: 125,
          bgcolor: "background.paper",
          border: "1px dashed grey",
          position: "relative",
          mb: 2,
        }}
      >
        {url && (
          <img
            style={{ width: "100%", height: "100%" }}
            alt={url}
            src={`${GetUrl(url)}?w=248&fit=crop&auto=format&dpr=2 2x`}
          />
        )}
        <Stack
          direction="row"
          justifyContent="center"
          alignItems="center"
          spacing={2}
          sx={{
            height: "100%",
            position: "absolute",
            top: 0,
            left: 0,
            width: "100%",
            height: "100%",
          }}
        >
          <IconButton
            color="#fff"
            aria-label="upload picture"
            component="label"
            size="small"
            onClick={showModalUpload}
          >
            <CloudUpload sx={{ mr: 1 }} />
            <label>Upload</label>
          </IconButton>
        </Stack>
      </Box>
      <ImageUploadDialog
        id="ringtone-menu"
        keepMounted
        open={open}
        onClose={handleClose}
        onSave={handleUpload}
        fileSelected={fileSelected}
      />
    </Box>
  );
}
