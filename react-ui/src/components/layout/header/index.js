import { css } from '@emotion/react'
import AppBar from '@mui/material/AppBar';
import Container from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Link from '@mui/material/Link';
import { publicRoutes as routes } from '~/routes';

const style = css`
  color: hotpink;
`
const preventDefault = (event) => event.preventDefault();

function Header() {
    return (
        <AppBar position="static">
            <Container maxwidth="sm">
                <Box
                    sx={{
                        typography: 'body1',
                        '& > :not(style) + :not(style)': {
                            ml: 2,
                        },
                    }}
                    onClick={preventDefault}
                >
                    {routes.map((item, index) => (
                        <Link
                            key={index}
                            sx={{ my: 2, color: 'white', display: 'inline-block' }}
                            href={item.path}
                        >
                            {item.name}
                        </Link>
                    ))}
                </Box>
            </Container>
        </AppBar>
    )
}

export default Header;