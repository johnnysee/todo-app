import {
  Button,
  Checkbox,
  Container,
  List,
  ListItem,
  TextField,
} from "@mui/material";
import axios from "axios";
import { useEffect, useState } from "react";

const URL = "https://localhost:7247/api/todoitems";

interface AppProps {
  id: number;
  name: string;
  isComplete: boolean;
}

export const App: React.FC = () => {
  const [data, setData] = useState<AppProps[]>([]);
  const [input, setInput] = useState("");
  const [isChanged, setIsChanged] = useState(false);

  useEffect(() => {
    axios
      .get(URL)
      .then((res) => {
        setData(res.data);
      })
      .catch((err) => console.log(err));
  }, [isChanged]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const inputValue = e.target.value;
    setInput(inputValue);
  };

  const handleClick = async () => {
    await axios
      .post(URL, { name: input, isComplete: false })
      .catch((e) => console.log(e));
  };

  const handleDelete = async (id: number) => {
    await axios.delete(`${URL}/${id}`).catch((e) => console.log(e));
  };

  return (
    <Container>
      <TextField
        onChange={handleChange}
        id="standard-basic"
        label="Fill in a task"
        variant="standard"
        value={input}
      />
      <Button
        onClick={() => {
          handleClick();
          setInput("");
          setIsChanged(!isChanged);
        }}
        variant="contained"
      >
        Add Task
      </Button>
      {data && (
        <>
          <h1>To-Do List</h1>
          <List>
            {data.map((d) => (
              <ListItem key={d.id}>
                {d.name}
                <Checkbox
                  onChange={() => {
                    handleDelete(d.id);
                    setIsChanged(!isChanged);
                  }}
                  value={d.isComplete}
                />
              </ListItem>
            ))}
          </List>
        </>
      )}
    </Container>
  );
};
