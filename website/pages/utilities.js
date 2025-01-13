
  async function send(path, body) {
  console.log('send');
  
  let response = await fetch(
    `/${path}`,
    {
      method: "POST",
      body: JSON.stringify(body),
      headers: {
        "X-Is-Custom": "true"
      }
    }
  );

  try {
    let obj = await response.json();
    let data = obj.data ?? null;
    return data;
  }
  catch {
    return null;
  }
}