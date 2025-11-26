use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> Option<String> {
    match part {
        Part::P1 => Day25::solve_part_one(input),
        Part::P2 => Day25::solve_part_two(input),
    }
}

struct Day25;
impl Solution for Day25 {
    fn solve_part_one(input: String) -> Option<String> {
        let mut keys = vec![];
        let mut locks = vec![];
        for paragraph in input.split("\n\n") {
            let mut columns: [u8; 5] = [0; 5];

            for (i, line) in paragraph.lines().enumerate() {
                match i {
                    0 => continue,
                    6 => {
                        if line == "....." {
                            locks.push(columns);
                        } else {
                            keys.push(columns);
                        }
                    }
                    _ => {
                        for (i, ch) in line.char_indices() {
                            if ch == '#' {
                                columns[i] += 1;
                            }
                        }
                    }
                }
            }
        }

        Some(
            locks
                .iter()
                .fold(0, |count, lock| {
                    count
                        + keys
                            .iter()
                            .filter(|key| lock.iter().enumerate().all(|(i, c)| c + key[i] <= 5))
                            .count()
                })
                .to_string(),
        )
    }

    fn solve_part_two(_input: String) -> Option<String> {
        None
    }
}
