import {useState} from 'react'
import './App.css'

function App() {
    const [input, setInput] = useState('')
    const [result, setResult] = useState('')
    const [error, setError] = useState('')
    const [isLoading, setIsLoading] = useState(false)

    const convertNumber = async () => {
        setIsLoading(true)
        setError('')

        try {
            const response = await fetch(`/convert?input=${encodeURIComponent(input)}`)

            if (!response.ok) {
                throw new Error('The number could not be converted.')
            }

            setResult(await response.text())
        } catch (requestError) {
            setResult('')
            setError(requestError.message)
        } finally {
            setIsLoading(false)
        }
    }

    return (
        <>
            <section id="center">
                <div className="hero">
                </div>
                <div>
                    <h1>Number to Words</h1>
                    <p>
                        This application is converting input numbers into English words.
                    </p>
                    <p>
                        Instructions: Please input numbers with no more than 2 decimal digits.
                    </p>
                </div>
                <input
                    id="number-input"
                    className="number-input"
                    type="text"
                    inputMode="decimal"
                    placeholder="e.g. 123.45"
                    aria-label="Number to convert"
                    value={input}
                    onChange={(event) => setInput(event.target.value)}
                />
                    
                <button
                    type="button"
                    className="counter"
                    onClick={convertNumber}
                    disabled={isLoading}
                >
                    {isLoading ? 'Converting...' : 'Convert'}
                </button>

                {result && <p className="conversion-result">{result}</p>}
                {error && <p className="conversion-error" role="alert">{error}</p>}
            </section>

            <div className="ticks"></div>
        </>
    )
}

export default App
