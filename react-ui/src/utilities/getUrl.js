export default function GetUrl(vituralPath) {
  return `${process.env.REACT_APP_BASE_URL}${vituralPath}`;
}
