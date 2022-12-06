use std::str::SplitTerminator;

pub trait StrUtils {
    fn paragraphs(&self) -> SplitTerminator<&str>;
}

impl StrUtils for str {
    fn paragraphs(&self) -> SplitTerminator<&str> {
        self.split_terminator("\n\n")
    }
}
