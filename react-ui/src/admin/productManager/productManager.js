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
import { getProducts, createProduct } from "~/services/productService";
import ImageUpload from "~/components/imageUpload";
import GetUrl from "~/utilities/getUrl";
import { ProductCreateOrUpdate } from "~/admin/productManager";

export default function ProductManager() {
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [price, setPrice] = useState("");
  const [quantity, setQuantity] = useState("");
  const [url, setUrl] = useState("");
  const [images, setImages] = useState([]);
  const [open, setOpen] = useState(false);

  useEffect(() => {
    loadData();
  }, []);

  
  const handleClickOpen = () => {
    setOpen(true);
  };


  const loadData = async () => {
    const param = {
      name: "abc",
    };
    const result = await getProducts("Product", param);
    console.log("result", result);
    result && result.data && setImages(result.data);
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
      await loadData();
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <Container maxWidth="lg">
      
      <Button variant="outlined" onClick={handleClickOpen}>
        Open max-width dialog
      </Button>
      <ProductCreateOrUpdate open={open}/>
    </Container>
  );
}
