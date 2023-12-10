let url = ''
const form = document.getElementById('form')
const userPassword = document.getElementById('userPassword')
const urlField = document.getElementById('url')
const submit = document.getElementById('submit')
const newPass = document.getElementById('result')
const copy = document.getElementById('copy')
const resultContainer = document.getElementById('result-container')
const resultLabel = document.getElementById('result-label')

const callback = (data) => {
  newPass.innerText = data
}

const getPass = async (key, url) => {
  try {
    const path = `https://localhost:7208/gen-pwd`
    const response = await fetch(path, {
      method: 'POST',
      headers: {
        'Content-Type': 'Application/json',
      },
      body: JSON.stringify({
        title: url,
        masterKey: key
      })
    })
    console.log(response)
    if(response.ok){
      const data = await response.json()
      callback(data)
    }
    else{
      console.log('API error')
    }
  } catch (err) {
    console.error(err)
  }
}

const copyToClipboard = () => {
  const textArea = document.createElement("textarea")
  textArea.value = newPass.innerText
  document.body.appendChild(textArea)
  textArea.select()
  document.execCommand("copy")
  document.body.removeChild(textArea)
  newPass.innerText = 'Copied!'
}

//urlFelid.value = url

submit.onclick = (async () => {
  form.remove()
  newPass.style.visibility = 'visible'
  await getPass(userPassword.value, urlField.value)
  copy.style.visibility = 'visible'
  resultContainer.classList.add('white-background')
  resultLabel.innerText = 'Your password is:'
  copy.onclick = () => copyToClipboard()
})

const onOpen = (() => {
  document.addEventListener('keydown', () =>{
    if(event.keyCode === 13){
      submit.click()
    }
  })
  chrome.runtime.sendMessage('getURL', (response) => {
    url = response
    urlField.value = url
  })
})()
