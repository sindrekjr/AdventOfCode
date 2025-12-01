use std::str::SplitTerminator;

pub trait StrUtils {
    fn digits(&self) -> Vec<u8>;
    fn paragraphs(&self) -> SplitTerminator<'_, &str>;
}

impl StrUtils for str {
    fn digits(&self) -> Vec<u8> {
        self.chars().map(|c| c as u8 - 0x30).collect()
    }

    fn paragraphs(&self) -> SplitTerminator<'_, &str> {
        self.split_terminator("\n\n")
    }
}
