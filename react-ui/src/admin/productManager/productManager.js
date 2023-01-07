import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import { useEffect, useState } from "react";
import {
  getProducts,
  getById,
  createProduct,
  updateProduct,
  deleteProduct,
} from "~/services/productService";
import GetUrl from "~/utilities/getUrl";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import IconButton from "@mui/material/IconButton";
import DeleteIcon from "@mui/icons-material/Delete";
import EditIcon from "@mui/icons-material/Edit";
import Paper from "@mui/material/Paper";
import { ProductCreateOrUpdate } from "~/admin/productManager";
import { apiStatus } from "~/enums/apiStatus";
import { getProfile } from "~/services/userService";

function createData(name, calories, fat, carbs, protein) {
  return {
    name,
    calories,
    fat,
    carbs,
    protein,
  };
}

const rows = [
  createData("Frozen yoghurt", 159, 6.0, 24, 4.0),
  createData("Ice cream sandwich", 237, 9.0, 37, 4.3),
  createData("Eclair", 262, 16.0, 24, 6.0),
  createData("Cupcake", 305, 3.7, 67, 4.3),
  createData("Gingerbread", 356, 16.0, 49, 3.9),
];

export default function ProductManager() {
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [price, setPrice] = useState("");
  const [quantity, setQuantity] = useState("");
  const [url, setUrl] = useState("");
  const [products, setProducts] = useState([]);
  const [open, setOpen] = useState(false);

  useEffect(() => {
    loadData();
  }, []);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const handleEdit = async (id) => {
    const result = await getById("Product", id);
  };

  const handleDelete = async (id) => {
    const result = await deleteProduct("Product", id);
    if (result.status === apiStatus.success) {
      setProducts((prev) => {
        return prev.filter((p) => p.id !== id);
      });
    }
  };

  const loadData = async () => {
    const param = {
      name: "abc",
    };
    const result = await getProducts("Product", param);
    const result1 = await getProfile("User/GetProfile");
    console.log("result", result);
    result && result.data && setProducts(result.data);
  };

  return (
    <Box>
      <Button variant="outlined" onClick={handleClickOpen} sx={{ mb: 2 }}>
        Create
      </Button>
      <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650 }} aria-label="simple table">
          <TableHead>
            <TableRow sx={{ backgroundColor: "#ccc" }}>
              <TableCell>Product Name</TableCell>
              <TableCell align="right">Image</TableCell>
              <TableCell align="right">Description</TableCell>
              <TableCell align="right">Price</TableCell>
              <TableCell align="right">Quantity</TableCell>
              <TableCell align="right">Action</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {products.map((row) => (
              <TableRow
                key={row.id}
                sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
              >
                <TableCell component="th" scope="row">
                  {row.name}
                </TableCell>
                <TableCell align="right">
                  <img
                    src={GetUrl(row.avatarUrl)}
                    alt={row.avatarUrl}
                    css={{
                      objectFit: "contain",
                      maxWidth: "125px",
                    }}
                  />
                </TableCell>
                <TableCell align="right">{row.description}</TableCell>
                <TableCell align="right">{row.price}</TableCell>
                <TableCell align="right">{row.quantity}</TableCell>
                <TableCell align="right">
                  <IconButton
                    aria-label="edit"
                    color="primary"
                    onClick={() => handleEdit(row.id)}
                  >
                    <EditIcon />
                  </IconButton>
                  <IconButton
                    aria-label="delete"
                    color="error"
                    onClick={() => handleDelete(row.id)}
                  >
                    <DeleteIcon />
                  </IconButton>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
      <ProductCreateOrUpdate
        open={open}
        onClose={handleClose}
        onLoadData={loadData}
      />
    </Box>
  );
}
