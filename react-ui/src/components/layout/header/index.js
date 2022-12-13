import { css } from '@emotion/react'

const style = css`
  color: hotpink;
`
function Header() {
    return <h2 css={style}>Header</h2>
}

export default Header;